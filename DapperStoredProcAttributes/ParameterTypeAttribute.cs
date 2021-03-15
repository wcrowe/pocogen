using System;
using System.Data;

namespace PortalData.Utils
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class ParameterTypeAttribute : Attribute
    {

        //Overloaded constructors are done purposefully , changing it to ParameterTypeAttribute(ParameterDirection direction= ParameterDirection.Input, bool ignoreField=false ) makes to look the place where the attribute is used funny. more over , I do not want to recompile the target assemblies if the signature changes 

        public ParameterTypeAttribute() : this(ParameterDirection.Input, false)
        {

        }

        public ParameterTypeAttribute(bool ignoreField) : this(ParameterDirection.Input, ignoreField)
        {

        }

        public ParameterTypeAttribute(ParameterDirection direction) : this(direction, false)
        {

        }

        public ParameterTypeAttribute(ParameterDirection direction, bool ignoreField)
        {
            Direction = direction;
            IgnoreField = ignoreField;

        }

        public ParameterDirection Direction { get; set; }
        public bool IgnoreField { get; set; }
    }
}