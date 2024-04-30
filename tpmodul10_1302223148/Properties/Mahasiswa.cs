using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpmodul10_1302223148
{
    public class Mahasiswa
    {
        public int Id { get; set; }
        /*private String? nama { get; set; }
        private String? nim { get; set; }
        public List<String>? course { get; set; }
        public int year {  get; set; }*/

        public String? Nama
        { get; set; }

        public String? Nim { get; set; }
        public List<String>? Course { get; set; }
        public int Year { get; set; }
    }
}