using Discord.Commands;
using Discord.Models;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Discord
{
    public class PersonaService
    {
        private readonly SemaphoreSlim SlowStuffSemaphore = new SemaphoreSlim(1, 1);

        private string _dataDirectory { get; }
        private string _personaFile => Path.Combine(_dataDirectory, "personas.json");

        // DiscordSocketClient and CommandService are injected automatically from the IServiceProvider
        public PersonaService()
        {
            _dataDirectory = Path.Combine(AppContext.BaseDirectory, "data");

            if (!Directory.Exists(_dataDirectory))
                Directory.CreateDirectory(_dataDirectory);
            if (!File.Exists(_personaFile))
                File.Create(_personaFile).Dispose();
        }
        
        public AddStatus AddPersona(ulong userId, string eauserId, string personaId)
        {
            SlowStuffSemaphore.WaitAsync();
            var result = AddStatus.Success;
            try {
                // Deserialize current personas
                var personas = JsonConvert.DeserializeObject<List<Persona>>(File.ReadAllText(_personaFile));

                // Null check
                personas = personas ?? new List<Persona>();

                // Check if current user already has a persona saved
                var persona = personas.FirstOrDefault(x => x.DiscordUserId == userId);
                if (persona != null) {
                    persona.PersonaId = personaId;
                    persona.EAUserId = eauserId;
                    result = AddStatus.Overwritten;
                }
                else {
                    personas.Add(new Persona { DiscordUserId = userId, EAUserId = eauserId, PersonaId = personaId });
                    result = AddStatus.Success;
                }

                // Save the changes
                File.WriteAllText(_personaFile, JsonConvert.SerializeObject(personas));
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                result = AddStatus.Fail;
            }
            finally {
                SlowStuffSemaphore.Release();
            }

            return result;
        }

        public bool TryGetPersonaId(ulong userId, out Persona persona) {
            try {
                // Deserialize current personas
                var personas = JsonConvert.DeserializeObject<List<Persona>>(File.ReadAllText(_personaFile));

                // Search for persona
                persona = personas.FirstOrDefault(x => x.DiscordUserId == userId);

                return persona != null;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);

                persona = null;
                return false;
            }
        }
    }
}
