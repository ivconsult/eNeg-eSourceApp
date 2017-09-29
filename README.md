# eNeg eSourceApp

## Description
eSourceApp connects [eNeg](https://github.com/ivconsult/eNeg) with the [negPOINT eSourcing System](http://www.negpoint.com/en/). It allows to include results and events happening on negPOINT into the whole negotiation flow on [eNeg](https://github.com/ivconsult/eNeg).

For more details please find the [Software Requirements Specification](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/SRS_eNeg_Negotiation_Framework.docx), the [Technical Design Specification](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/eNeg_TDS_KR.docx), the [Architecture](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/eNEG%20Infrastructure%20logical%20Architecture.docx) and some videos explaining the uses cases of eNeg in [eNeg Documentation](https://github.com/ivconsult/eNeg/tree/master/eNeg%20Documentation).

## Features

eSourceApp supports the user in creating eAuctions and eTenders via [negPOINT eSourcing System](http://www.negpoint.com/en/):

* Define the metadata of an event (auction or tender)
* Handover to [negPOINT eSourcing System](http://www.negpoint.com/en/) to create an event
* Include events/results from [negPOINT eSourcing System](http://www.negpoint.com/en/) in [eNeg](https://github.com/ivconsult/eNeg)

## Setting Development Environment

* A .NET Integrated Development Environment (IDE) such as Visual Studio or the free Visual Web Developer Express
* Install Microsoft Silverlight runtime for [windows](https://go.microsoft.com/fwlink/?LinkId=229324). (This is the runtime that’s required for Silverlight applications).
* Install [Silverlight Toolkit](https://silverlight.codeplex.com/releases/view/78435)
* Install [Silverlight SDK](https://www.microsoft.com/en-us/download/details.aspx?id=28359)
* Install [Silverlight Tools for VS 2010](https://www.microsoft.com/en-us/download/details.aspx?id=28358) (Optional)
* Install [Expression Blend](https://www.microsoft.com/en-eg/download/details.aspx?id=3062). This is a design tool that allows users to interact with Silverlight.

## References
### Following open-source projects were used:
* [Clog Client Logging](http://clog.codeplex.com) under [LGPL](http://clog.codeplex.com/license)
* [MVVM Light Toolkit](http://www.mvvmlight.net) under [MIT License](http://mvvmlight.codeplex.com/license)
* [iTextSharp](https://github.com/itext/itextsharp) under [AGPL](https://github.com/itext/itextsharp/blob/develop/LICENSE.md)
* [Rhino Mocks](https://github.com/ayende/rhino-mocks) under [BSD 3-clause "New" or "Revised" License](https://github.com/ayende/rhino-mocks/blob/master/license.txt)
* [silverPDF](https://silverpdf.codeplex.com/) under [MIT License](https://silverpdf.codeplex.com/license)

