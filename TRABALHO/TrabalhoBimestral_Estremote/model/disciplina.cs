using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoBimestral_Estremote.model
{
    internal class disciplina
    {

        public disciplina()
        {


        }

        public int coddisciplina { get; set; }

        public string nomedisciplina { get; set; }

        public string ementa { get; set; }
         
        public string cargahoraria { get; set; }

        public string bibliografia { get; set; }

        public byte[] fotodisciplina { get; set; }

    }
}
