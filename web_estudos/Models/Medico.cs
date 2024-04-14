using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_estudos.Models
{
    //Medico class
    public class Medico
    {
        public string CRM { get; set; }
        public string Nome { get; set;}
        public List<string> Especialidades { get; set;}

        public Medico(string _crm)
        {
            this.CRM = _crm;
            this.Especialidades = new List<string>();
        }
    }
}