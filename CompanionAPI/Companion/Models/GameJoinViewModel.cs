using Newtonsoft.Json;

namespace CompanionAPI.Models
{
    public class GameJoinViewModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// GAMEMANAGER_ERR_INVALID_GAME_ID: o.noServerFound,
        /// GAMEMANAGER_ERR_GAME_FULL: o.gameFull,
        /// GAMEMANAGER_ERR_ALREADY_GAME_MEMBER: o.alreadyInSession,
        /// GAMEMANAGER_ERR_INVALID_GAME_STATE_ACTION: o.invalidGameState,
        /// GAMEMANAGER_ERR_PLAYER_NOT_FOUND: o.playerNotFound,
        /// GAMEMANAGER_ERR_PLAYER_BANNED: o.banned,
        /// GAMEMANAGER_ERR_GAME_ENTRY_CRITERIA_FAILED: o.rankCriteriaFailed,
        /// GAMEMANAGER_ERR_GAME_PROTOCOL_VERSION_MISMATCH: o.wrongVersion,
        /// GAMEMANAGER_ERR_SPECTATOR_SLOTS_FULL: o.roleFull,
        /// GAMEMANAGER_ERR_RESERVATION_ALREADY_EXISTS: o.reservationAlreadyExists,
        /// GAMEMANAGER_ERR_ROLE_NOT_ALLOWED: o.roleNotAllowed,
        /// GAMEMANAGER_ERR_ROLE_FULL: o.roleFull,
        /// GAMEMANAGER_ERR_ROLE_CRITERIA_FAILED: o.roleCriteriaFailed,
        /// GAMEMANAGER_ERR_FAILED_REPUTATION_CHECK: o.failedReputationCheck
        /// </summary>
        [JsonProperty("errorResponse")]
        public string ErrorResponse { get; set; }
    }
}
