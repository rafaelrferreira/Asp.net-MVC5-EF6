using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaMVC.Models
{
    public class TelefoneV2
    {
        [DisplayName("Contato")]
        public int idContato { get; set; }
        public IEnumerable<SelectListItem> lContatos { get; set; }
        public string telefone { get; set; }
    }
}