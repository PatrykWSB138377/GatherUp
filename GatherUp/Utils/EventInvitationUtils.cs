using GatherUp.Models;

namespace GatherUp.Utils
{
    public static class EventInvitationUtils
    {

        private static readonly Dictionary<InvitationStatus, string> _statusTranslationMap = new Dictionary<InvitationStatus, string>
        {
            {InvitationStatus.Pending, "Oczekuje na odpowiedź"},
            {InvitationStatus.Accepted, "Zaakceptowano"},
            {InvitationStatus.Rejected, "Odrzucono"},
        };

        private static readonly Dictionary<InvitationStatus, string> _statusToClassMap = new Dictionary<InvitationStatus, string>
        {
            {InvitationStatus.Pending, "bg-secondary"},
            {InvitationStatus.Accepted, "bg-success"},
            {InvitationStatus.Rejected, "bg-danger"},
        };

        public static string TranslateStatus(InvitationStatus status)
        {
           return _statusTranslationMap[status];
        }

        public static string GetBadgeClasses(InvitationStatus status)
        {
            return $"badge rounded-pill {_statusToClassMap[status]}";
        }
    }
}
