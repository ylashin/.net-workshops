using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.DynamicParsers
{
    public class ColumnAttribute : Attribute
    {
        public int ColumnIndex { get; set; }

        public ColumnAttribute(int columnIndex)
        {
            ColumnIndex = columnIndex;
        }
    }
    public class FieldAttribute : Attribute
    {
        public bool IsRepeating { get; set; }
        public char Delimeter { get; set; }
        public FieldAttribute(bool isRepeating, char delimeter)
        {
            IsRepeating = isRepeating;
            Delimeter = delimeter;
        }
    }
}
