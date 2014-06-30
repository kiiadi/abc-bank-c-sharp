using System;

namespace AbcBank
{
    public class DateProvider
    {
        private static DateProvider _instance = null;
        private static object _objLocker = new object();
        private DateTimeKind _mode = DateTimeKind.Local;
        private DateTime? _fixedTime = new DateTime?();

        private DateProvider()
        {

        }
        public static DateProvider Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_objLocker)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = new DateProvider();
                }
                return _instance;
            }
        }

        #region IDateProvider impl
        public void SetKind(DateTimeKind kind)
        {
            _mode = kind;
        }
        public void SetFixed(DateTime fixedTime)
        {
            _fixedTime = fixedTime;
        }
        public DateTime now()
        {
            if (_fixedTime != null)
                return _fixedTime.Value;

            if (_mode == DateTimeKind.Local)
                return DateTime.Now;

            if (_mode == DateTimeKind.Utc)
                return DateTime.UtcNow;
            
            return DateTime.Now;
        }
        #endregion
    }
}
