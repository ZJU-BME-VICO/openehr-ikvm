ikvmc xpp3_min-1.1.4c.jar -target:library
ikvmc xml-apis-1.0.b2.jar -target:library
ikvmc stax-api-1.0.1.jar -target:library -r:xml-apis-1.0.b2.dll
ikvmc log4j-1.2.16.jar -target:library -r:xml-apis-1.0.b2.dll
ikvmc junit-4.5.jar -target:library
ikvmc junit-3.8.1.jar -target:library
ikvmc jsr173_api-1.0.jar -target:library -r:xml-apis-1.0.b2.dll
ikvmc xmlbeans-2.3.0.jar -target:library -r:jsr173_api-1.0.dll -r:xml-apis-1.0.b2.dll
ikvmc xml-binding-1.0.5-SNAPSHOT-sources.jar -target:library -r:xmlbeans-2.3.0.dll -r:jsr173_api-1.0.dll -r:xml-apis-1.0.b2.dll
ikvmc stax-1.1.1-dev.jar -target:library -r:jsr173_api-1.0.dll -r:xml-apis-1.0.b2.dll
ikvmc saxon-8.7.jar -target:library -r:xml-apis-1.0.b2.dll -r:jsr173_api-1.0.dll
ikvmc xmlbeans-xpath-2.3.0.jar -target:library -r:saxon-8.7.dll -r:xml-apis-1.0.b2.dll -r:xmlbeans-2.3.0.dll
ikvmc oet-parser-1.0.5-SNAPSHOT-sources.jar -target:library -r:xmlbeans-2.3.0.dll -r:jsr173_api-1.0.dll -r:xml-apis-1.0.b2.dll
ikvmc jsr-275-0.9.4.jar -target:library
ikvmc measure-serv-1.0.5-SNAPSHOT.jar -target:library -r:jsr-275-0.9.4.dll
ikvmc joda-time-2.2.jar -target:library
ikvmc jdom-1.0.jar -target:library -r:xml-apis-1.0.b2.dll
ikvmc dom4j-1.6.1.jar -target:library -r:xml-apis-1.0.b2.dll -r:jsr173_api-1.0.dll -r:xpp3_min-1.1.4c.dll
ikvmc xstream-1.3.1.jar -target:library -r:jsr173_api-1.0.dll -r:dom4j-1.6.1.dll -r:xml-apis-1.0.b2.dll -r:jdom-1.0.dll -r:xpp3_min-1.1.4c.dll -r:joda-time-2.2.dll
ikvmc commons-lang-2.6.jar -target:library
ikvmc openehr-rm-core-1.0.5-SNAPSHOT.jar -target:library -r:commons-lang-2.6.dll -r:joda-time-2.2.dll -r:measure-serv-1.0.5-SNAPSHOT.dll
ikvmc openehr-rm-domain-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll
ikvmc rm-builder-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-rm-domain-1.0.5-SNAPSHOT.dll -r:log4j-1.2.16.dll -r:commons-lang-2.6.dll
ikvmc openehr-aom-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll
ikvmc openehr-ap-1.0.5-SNAPSHOT.jar -target:library -r:commons-lang-2.6.dll -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll
ikvmc xml-serializer-1.0.5-SNAPSHOT.jar -target:library -r:jdom-1.0.dll -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll -r:openehr-ap-1.0.5-SNAPSHOT.dll
ikvmc oet-parser-1.0.5-SNAPSHOT.jar -target:library -r:xmlbeans-2.3.0.dll -r:jsr173_api-1.0.dll -r:xml-apis-1.0.b2.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-ap-1.0.5-SNAPSHOT.dll -r:log4j-1.2.16.dll
ikvmc mini-termserv-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll -r:log4j-1.2.16.dll -r:dom4j-1.6.1.dll
ikvmc xml-binding-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-rm-domain-1.0.5-SNAPSHOT.dll -r:log4j-1.2.16.dll -r:commons-lang-2.6.dll -r:xmlbeans-2.3.0.dll -r:rm-builder-1.0.5-SNAPSHOT.dll -r:measure-serv-1.0.5-SNAPSHOT.dll -r:mini-termserv-1.0.5-SNAPSHOT.dll -r:jsr173_api-1.0.dll -r:xml-apis-1.0.b2.dll
ikvmc rm-skeleton-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-domain-1.0.5-SNAPSHOT.dll -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:log4j-1.2.16.dll -r:oet-parser-1.0.5-SNAPSHOT.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll -r:openehr-ap-1.0.5-SNAPSHOT.dll -r:rm-builder-1.0.5-SNAPSHOT.dll -r:measure-serv-1.0.5-SNAPSHOT.dll -r:mini-termserv-1.0.5-SNAPSHOT.dll
ikvmc dadl-parser-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll
ikvmc dadl-binding-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:rm-builder-1.0.5-SNAPSHOT.dll -r:dadl-parser-1.0.5-SNAPSHOT.dll -r:log4j-1.2.16.dll -r:measure-serv-1.0.5-SNAPSHOT.dll -r:mini-termserv-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll -r:openehr-rm-domain-1.0.5-SNAPSHOT.dll
ikvmc commons-jxpath-1.3.jar -target:library -r:xml-apis-1.0.b2.dll -r:jdom-1.0.dll
ikvmc commons-io-2.3.jar -target:library
ikvmc archetype-validator-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll -r:openehr-ap-1.0.5-SNAPSHOT.dll -r:log4j-1.2.16.dll -r:mini-termserv-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll -r:openehr-rm-domain-1.0.5-SNAPSHOT.dll
ikvmc adl-serializer-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll -r:openehr-ap-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll
ikvmc adl-parser-1.0.5-SNAPSHOT.jar -target:library -r:openehr-rm-core-1.0.5-SNAPSHOT.dll -r:openehr-aom-1.0.5-SNAPSHOT.dll -r:openehr-ap-1.0.5-SNAPSHOT.dll -r:measure-serv-1.0.5-SNAPSHOT.dll -r:mini-termserv-1.0.5-SNAPSHOT.dll -r:commons-lang-2.6.dll
