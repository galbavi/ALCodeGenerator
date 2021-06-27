using System;
using System.Collections.Generic;
using System.Text;

namespace ALGenerator
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
