using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Sessions
{
    public class HttpSession : IHttpSession
    {
        private Dictionary<string, object> parameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.parameters = new Dictionary<string, object>();

        }

        public string Id { get; }

        public object GetParameter(string name)
        {
            if (!ContainsParameter(name))
            {

                return null;
            }

            return this.parameters[name];
        }


        public bool ContainsParameter(string name)
        {
            return this.parameters.ContainsKey(name);

        }

        public void AddParameter(string name, object parameter)
        {

            this.parameters[name] = parameter;
        }

        public void ClearParameters()
        {
            this.parameters.Clear();

        }

    }

}
