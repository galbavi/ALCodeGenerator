using System;
using System.Collections.Generic;
using System.Text;

namespace ALCodeGenerator
{
    enum DataClassification
    {
        ToBeClassified,
        CustomerContent,
        EndUserIdentifiableInformation,
        AccountData,
        EndUsePseudonymousIdentifiers,
        OrganizationIdentifiableInformation,
        SystemMetadata
    }
}
