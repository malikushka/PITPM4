using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITPM4
{
    [Table("Менеджер")]
    public class Menedjer
    {
            public int ID { get; set; }
            public string Имя { get; set; }
            public string Пароль { get; set; }
            public string Почта { get; set; }
            public string Телефон { get; set; }
            public string Должность { get; set; }
        
    }
}