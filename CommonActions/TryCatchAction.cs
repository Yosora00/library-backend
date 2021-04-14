using library_backend.Results;
using System;
using System.Threading.Tasks;

namespace library_backend.CommonActions
{
    public static class TryCatchAction<T> where T : ResultBase, new()
    {
        public static T Excute(
            Action act,
            Action<Exception> err = null,
            Action final = null)
        {
            var r = new T();
            try
            {
                act();
                r.isSuccess = true;
            }
            catch (Exception e)
            {
                r.isSuccess = false;
                r.message = e.Message;
                if (err != null)
                {
                    err(e);
                }
            }
            finally
            {
                if (final != null)
                {
                    final();
                }
            }
            return r;
        }
    }
}