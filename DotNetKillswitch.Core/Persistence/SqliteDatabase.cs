using System.IO;
using System.Web;

namespace DotNetKillswitch.Core.Persistence
{
    /// <summary>
    /// Default implementation of <see cref="IDatabaseResolver"/>.
    /// </summary>
    public class SqliteDatabase : IDatabaseResolver
    {
        #region IDatabaseResolver Members

        public string FilePath
        {
            get
            {
                string path = HttpRuntime.AppDomainAppPath;
                string dataPath = Path.Combine(path, Constants.DatabasePath);

                return dataPath;
            }
        }

        #endregion
    }
}
