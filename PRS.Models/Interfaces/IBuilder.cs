using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Interfaces
{
    interface IBuilder<T> where T : class
    { 
        T Build();
    }
}
