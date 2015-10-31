using System;

namespace SignalRChat.Dtos
{
    [Serializable]
    public class ModifiedBase
    {
        /// <summary>
        /// RowVersion
        /// </summary>
        public long? RowVersion { get; set; }
    }
}
