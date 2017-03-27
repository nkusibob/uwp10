using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Microsoft.EBC.SurfaceComponent.Data.Entities.DataEntities
{
    public static class ConstantsSurfaceComponent
    {
        /// <summary>
        /// KeySize for Encryption and Decryption of passwords
        /// </summary>
        public const int EncryptionKeySize = 1024;

        public const int HTTP_Timeout_Secs = 10;

        /// <summary>
        /// Salt size for Hashing passwords
        /// </summary>
        public const int SaltSize = 128;

        /// <summary>
        /// Authentication token header name
        /// </summary>
        public const string AuthenticationToken = "AuthenticationToken";

        /// <summary>
        /// Authorization enable property key
        /// </summary>
        public const string AuthorizationEnabled = "AuthorizationEnabled";

        /// <summary>
        /// Azure Log Transfer Time in seconds property key
        /// </summary>
        public const string LogTransferTimeInSeconds = "LogTransferTimeInSeconds";

        /// <summary>
        /// LogLevel property key in Azure service definition/configutation
        /// </summary>
        public const string LogLevel = "LogLevel";

        /// <summary>
        /// Mobile service method
        /// </summary>
        public static string GET_BRIEFINGS_BY_DATECENTER = "getbriefingsbydatecenter";

        /// <summary>
        /// Mobile service method
        /// </summary>
        public static string GET_BRIEFING_AGENDA = "GetBriefingAgenda";

        /// <summary>
        /// EBC Mobile service method name
        /// </summary>
        public static string GET_ATTENDEE_AGENDA = "GetEBCWin8AttendeeAgenda";

        /// <summary>
        /// EBC Mobile service method name
        /// </summary>
        public static string GET_ATTENDEE_BY_BRIEFING = "GetEBCWin8AttendeesByBriefing";

        /// <summary>
        /// EBC Mobile service method name
        /// </summary>
        public static string GET_EVAL_QUESTION = "GetEBCWin8EvalQuestions";

        /// <summary>
        /// EBC Mobile service method name
        /// </summary>
        public static string GET_INDUSTRY = "GetEBCWin8Industries";

        /// <summary>
        /// EBC Mobile service method name
        /// </summary>
        public static string UPDATE_WWBN_EVAL_ANSWER = "PostEBCWin8EvalAnswer";

        /// <summary>
        /// EBC Mobile service method name
        /// </summary>
        public static string EMAIL_CALENDAR_FILE = "PostEBCWin8Calendar";

        /// <summary>
        /// Access Control Generic URL
        /// </summary>
        public static string ACCESS_CONTROL_GENERIC_URL = "accesscontrol.windows.net";

        /// <summary>
        /// Filter for Evaluation Questions - During the Briefing
        /// </summary>
        public const string DuringBriefing = "During the Briefing";

        /// <summary>
        /// Filter for Evaluation Questions - Overall Briefing
        /// </summary>
        public const string OverallBriefing = "Overall Briefing";

        /// <summary>
        /// Filter for Evaluation Questions - EBC Facility
        /// </summary>
        public const string EBCFacility = "EBC Facility";

        /// <summary>
        /// Error message for not found error
        /// </summary>
        public const string NotFoundError = "Resource Not found";

        /// <summary>
        /// Error message for internal server error
        /// </summary>
        public const string InternalServerError = "An Error Occurred.";

        /// <summary>
        /// Outh Wrap protocal header name 
        /// </summary>
        public const string Wrap_Name = "wrap_name";


        public const string JsonMediaType = "application/json";

        /// <summary>
        /// Web Resource Authorization Protocol header name 
        /// </summary>
        public const string Wrap_Password = "wrap_password";

        /// <summary>
        /// Web Resource Authorization Protocol header name 
        /// </summary>
        public const string Wrap_Scope = "wrap_scope";

        /// <summary>
        /// Web Resource Authorization Protocol version
        /// </summary>
        public const string Wrap_Version = "WRAPv0.9";

        /// <summary>
        /// Web Resource Authorization Protocol Access Token Header Name 
        /// </summary>
        public const string Wrap_Access_Token = "wrap_access_token";
    }
    class Constants
    {
        public static readonly string SCOPE = "http://myebcmobile.servicebus.windows.net/";
        public static readonly string WRAP_PASSWORD = "Td/XX5hXjkEAfj1LeoiGng4foMkHS67D+C6bKRl4xpc=";
        public static readonly string WRAP_USERNAME = "owner";
        public static readonly string ACS_NAMESPACE = "myebcmobile-sb";



        public static readonly string BRIEFING_DETAILS_ID_URI = "https://myebcmobile.servicebus.windows.net/abhitest1/getbrfdetailbyid/{0}";
        public static readonly string AGENDA_BY_BRIEFING_DETAIL_ID_URI = "https://myebcmobile.servicebus.windows.net/abhitest1/getagendabybrfid/{0}/{1}";

        public static readonly string BRIFINGS_BY_CENTER_DATE = "https://myebcmobile.servicebus.windows.net/abhitest1/getbriefingsbydatecenter/{0}/{1}/{2}";
    }
}
