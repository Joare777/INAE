using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Models
{
    public class Reclamacao
    {

        public int id { get; set; }

        [Required]
        [Display(Name = "Nome Completo")]
        public string nomeCompleto { get; set; }

        [Display(Name = "Nacionalidade")]
        public string nacionalidade { get; set; }

        [Required]
        [Display(Name = "Tipo de Identificação")]
        public string tipoIdentificacao { get; set; }

        [Display(Name = "BI / Passaport")]
        public string identificacao { get; set; }

        [Display(Name ="Provincia")]
        public int provinciaId { get; set; }

        [Display(Name = "Hotel")]
        public int hotelId { get; set; }

        [Display(Name = "Texto")]
        public string texto { get; set; }

        public bool aprovado { get; set; }
        
        public int? fileId { get; set; }

        [ForeignKey ("hotelId")]
        [Display(Name ="Hotel")]
        public Hotel hotel { get; set; }

        [ForeignKey("fileId")]
        public FileDescription file { get; set; }
        

    }

    public class Hotel
    {
        public int id { get; set; }
        
        public string nome { get; set; }
        
        public int classificacao { get; set; }

        public int provinciaId { get; set; }

        [ForeignKey("provinciaId")]
        public Provincia provincia { get; set; }

        public ICollection<Reclamacao> listReclamacoes { get; set; }
    }

    public class Provincia
    {
        public int id { get; set; }

        public string sigla { get; set; }

        public string descricao { get; set; }

        public string notas { get; set; }
    }

    public class FileDescription
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string ContentType { get; set; }


    }
}
