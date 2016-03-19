using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5WithFullFramework.Models
{
 
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public Task()
        {
            
        }
    }
}
