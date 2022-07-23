using FinLib.Models.Constants.Database;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System.Text;

namespace FinLib.Providers.Logging
{
    public static class ConfigurationExtensions
    {
        public static void AddMethodCallTarget(this LoggingConfiguration configuration, LogLevel minLevel, LogLevel maxLevel, string methodName)
        {
            var methodTarget = new MethodCallTarget();
            methodTarget.ClassName = typeof(Configuration).AssemblyQualifiedName;
            methodTarget.MethodName = methodName;
            methodTarget.Parameters.Add(new MethodCallParameter("${level}"));
            methodTarget.Parameters.Add(new MethodCallParameter("${message}"));

            configuration.AddRule(minLevel, maxLevel, methodTarget);
            LogManager.Configuration = configuration.Reload();
        }

        public static void AddEventLogTarget(this LoggingConfiguration configuration, string targetName, LogLevel minLevel, LogLevel maxLevel
            , string categoryName, string sourceName, EventLogTarget eventLogTarget = null)
        {
            eventLogTarget ??= getEventLogTarget(targetName, categoryName, sourceName);

            configuration.AddRule(minLevel, maxLevel, eventLogTarget);
            LogManager.Configuration = configuration.Reload();
        }

        private static EventLogTarget getEventLogTarget(string targetName, string categoryName, string sourceName)
        {
            var eventLogTarget = new EventLogTarget()
            {
                Name = targetName,
                Log = categoryName,
                Source = sourceName,
                EventId = "${event-properties:item=event-id:whenEmpty=0}",
                EntryType = "${event-properties:item=event-type}",

                Layout = "MSG:${message}${newline}" +

                       "# REQ-IP:${aspnet-request-ip}${newline}" +
                       "# MACHINE-NAME:${machinename}${newline}" +
                       "# AUTH-USER:${aspnet-user-identity}${newline}" +
                       "# AUTH-USER-ID:${aspnet-user-id}${newline}" +

                       "# REQ:${aspnet-request-url:IncludePort=true:IncludeQueryString=true}${newline}" +
                       "# REQ-METHOD:${aspnet-request-method}${newline}" +
                       "# QUERYSTRING:${aspnet-request-querystring:QueryStringKeys=key1,id:OutputFormat=JSON}${newline}" +
                       "# REFERRER:${aspnet-request-referrer}${newline}" +
                       "# USERAGENT:${aspnet-request-useragent}${newline}" +
                       "# STATUS:${aspnet-response-statuscode}${newline}" +

                       "# EXP:${exception:format=tostring}${newline}" +
                       "# CALL-SITE:${callsite:fileName=true:includeSourcePath=false:skipFrames=1:cleanNamesOfAsyncContinuations=true}${newline}" +
                       "# CONTEXT-TRACE-ID:${aspnet-TraceIdentifier}${newline}" +
                       "# FINLIB-TRACE-ID:${event-properties:item=finlib-trace-id}${newline}"
            };

            return eventLogTarget;
        }

        public static void AddDatabaseTarget(this LoggingConfiguration configuration, string targetName, LogLevel minLevel, LogLevel maxLevel
            , string connectionString
            , DatabaseTarget databaseTarget = null)
        {
            databaseTarget ??= getDatabaseTarget(targetName, connectionString);
            configuration.AddRule(minLevel, maxLevel, databaseTarget);

            LogManager.Configuration = LogManager.Configuration.Reload();
        }

        private static DatabaseTarget getDatabaseTarget(string targetName, string connectionString)
        {
            var dbTarget = new DatabaseTarget()
            {
                Name = targetName,
                ConnectionString = connectionString,

                CommandType = System.Data.CommandType.Text,

                CommandText =
$@"INSERT INTO {ObjectNames.TableName_Logging}
(
      Level
    , Category
    , EventId
    , EventType

    , Logger

    , Message 
    , CustomData

    , EntityName
    , EntityTitle
    , OldValue
    , NewValue

    , RemoteIpAddress
    , AuthenticatedUserName
    , UserId

    , Request
    , HttpMethod
    , QueryString
    , HttpReferrer
    , UserAgent
    , StatusCode
    , ContentType
    , AllRequestHeaders

    , Exception
    , Callsite
    , ContextTraceId
    , IdpTraceId
) 
VALUES
(
      @Level
    , NullIf(@Category, '')
    , NullIf(@EventId, '')
    , NullIf(@EventType, '')

    , @Logger

    , NullIf(@Message, '')
    , NullIf(@CustomData, '')

    , NullIf(@EntityName, '')
    , NullIf(@EntityTitle, '')
    , NullIf(@OldValue, '')
    , NullIf(@NewValue, '')
    
    , NullIf(@RemoteIpAddress, '')
    , NullIf(@AuthenticatedUserName, '')
    , NullIf(@UserId, '')

    , NullIf(@Request, '')
    , NullIf(@HttpMethod, '')
    , NullIf(@QueryString, '')
    , NullIf(@HttpReferrer, '')
    , (CASE WHEN ISNULL(@Exception,'') ='' THEN NULL ELSE NullIf(@UserAgent,'') END)
    , NullIf(@StatusCode, '')
    , (CASE WHEN ISNULL(@Exception, '') ='' THEN NULL ELSE NullIf(@ContentType,'') END)
    , (CASE WHEN ISNULL(@Exception, '') ='' THEN NULL ELSE NullIf(@AllRequestHeaders, '') END)

    , NullIf(@Exception, '')
    , (CASE WHEN ISNULL(@Exception,'') ='' THEN NULL ELSE NullIf(@Callsite,'') END)
    , NullIf(@ContextTraceId, '')
    , NullIf(@IdpTraceId, '')
)"
            }; 

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Level", new SimpleLayout("${level}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Category", new SimpleLayout("${event-properties:item=category}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@EventId", new SimpleLayout("${event-properties:item=event-id}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@EventType", new SimpleLayout("${event-properties:item=event-type}")));

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Logger", new SimpleLayout("${logger}")));

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Message", new SimpleLayout("${message}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@CustomData", new SimpleLayout("${event-properties:item=custom-data}")));

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@EntityName", new SimpleLayout("${event-properties:item=entity-name}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@EntityTitle", new SimpleLayout("${event-properties:item=entity-title}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@OldValue", new SimpleLayout("${event-properties:item=old-value}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@NewValue", new SimpleLayout("${event-properties:item=new-value}")));

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@RemoteIpAddress", new SimpleLayout("${aspnet-request-ip}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@AuthenticatedUserName", new SimpleLayout("${aspnet-user-identity}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@UserId", new SimpleLayout("${aspnet-user-id}")));

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Request", new SimpleLayout("${aspnet-request-url:IncludePort=true:IncludeQueryString=true}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@HttpMethod", new SimpleLayout("${aspnet-request-method}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@QueryString", new SimpleLayout("${aspnet-request-querystring:QueryStringKeys=key1,id:OutputFormat=JSON}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@HttpReferrer", new SimpleLayout("${aspnet-request-referrer}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@UserAgent", new SimpleLayout("${aspnet-request-useragent}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@StatusCode", new SimpleLayout("${aspnet-response-statuscode}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@ContentType", new SimpleLayout("${aspnet-request-contenttype}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@AllRequestHeaders", new SimpleLayout("${aspnet-request-all-headers}")));

            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Exception", new SimpleLayout("${exception:format=tostring}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@Callsite", new SimpleLayout("${callsite:fileName=true:includeSourcePath=false:skipFrames=1:cleanNamesOfAsyncContinuations=true}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@ContextTraceId", new SimpleLayout("${aspnet-TraceIdentifier}")));
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@IdpTraceId", new SimpleLayout("${event-properties:item=finlib-trace-id}")));

            return dbTarget;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="fileTarget"></param>
        public static void AddFileTarget(this LoggingConfiguration configuration
            , string targetName
            , LogLevel minLevel, LogLevel maxLevel
            , bool isInAppSameDirectory
            , string absoluteDirectoryPath
            , string fileName
            , FileTarget fileTarget = null)
        {
            fileTarget ??= getFileTarget(targetName, isInAppSameDirectory, absoluteDirectoryPath, fileName);

            configuration.AddRule(minLevel, maxLevel, fileTarget);
            LogManager.Configuration = LogManager.Configuration.Reload();
        }

        private static FileTarget getFileTarget(string targetName, bool isInAppSameDirectory, string absoluteDirectoryPath, string fileName)
        {
            string absoluteFileName;
            if (isInAppSameDirectory)
            {
                absoluteFileName = "${basedir}";
                absoluteFileName += $"/{fileName}";
            }
            else
            {
                absoluteFileName = $"{absoluteDirectoryPath}/{fileName}";
            }

            return new FileTarget()
            {
                Name = targetName,
                //FileName = "${basedir}/Logs/App.log",
                FileName = absoluteFileName,

                Layout =
                       ">>> ${longdate}${newline}" +

                       "# LEVEL:${level}${newline}" +
                       "# CATEGORY-ID:${event-properties:item=category}${newline}" +
                       "# EVENT-ID:${event-properties:item=event-id}${newline}" +
                       "# EVENT-TYPE:${event-properties:item=event-type}${newline}" +

                       "# MSG:${message}${newline}" +

                       "# REQ-IP:${aspnet-request-ip}${newline}" +
                       "# MACHINE-NAME:${machinename}${newline}" +
                       "# AUTH-USER:${aspnet-user-identity}${newline}" +
                       "# AUTH-USER-ID:${aspnet-user-id}${newline}" +

                       "# REQ:${aspnet-request-url:IncludePort=true:IncludeQueryString=true}${newline}" +
                       "# REQ-METHOD:${aspnet-request-method}${newline}" +
                       "# QUERYSTRING:${aspnet-request-querystring:QueryStringKeys=key1,id:OutputFormat=JSON}${newline}" +
                       "# REFERRER:${aspnet-request-referrer}${newline}" +
                       "# USERAGENT:${aspnet-request-useragent}${newline}" +
                       "# STATUS:${aspnet-response-statuscode}${newline}" +

                       "# EXP:${exception:format=tostring}${newline}" +
                       "# CALL-SITE:${callsite:fileName=true:includeSourcePath=false:skipFrames=1:cleanNamesOfAsyncContinuations=true}${newline}" +
                       "# CONTEXT-TRACE-ID:${aspnet-TraceIdentifier}${newline}" +
                       "# FINLIB-TRACE-ID:${event-properties:item=finlib-trace-id}${newline}" +
                       "---------------",

                Encoding = Encoding.UTF8,
                KeepFileOpen = true,
                ReplaceFileContentsOnEachWrite = false,

                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveFileName = "${basedir}/Logs/Archives/{#}.log",
                ArchiveNumbering = ArchiveNumberingMode.Date,
                //EnableArchiveFileCompression = true,
                //MaxArchiveFiles = 7,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="coloredConsoleTarget"></param>
        public static void AddColoredConsoleTarget(this LoggingConfiguration configuration, string targetName, LogLevel minLevel, LogLevel maxLevel, ColoredConsoleTarget coloredConsoleTarget = null)
        {
            coloredConsoleTarget ??= getColoredConsoleTarget(targetName);

            configuration.AddRule(minLevel, maxLevel, coloredConsoleTarget);
            LogManager.Configuration = LogManager.Configuration.Reload();
        }

        private static ColoredConsoleTarget getColoredConsoleTarget(string targetName)
        {
            return new ColoredConsoleTarget
            {
                Name = targetName,
                Header = "--- START OF LOG ---",
                Encoding = Encoding.UTF8,

                Footer = "---",
            };
        }
    }
}
