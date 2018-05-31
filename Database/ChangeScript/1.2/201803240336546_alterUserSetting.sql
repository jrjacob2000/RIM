﻿use RIM
go

ALTER TABLE [dbo].[UserSettings] ADD [AdjustPrefix] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [AdjustNumber] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [CustomerReturnPrefix] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [CustomerReturnNumber] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [SupplierReturnPrefix] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [SupplierReturnNumber] [nvarchar](max)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201803240336546_alterUserSetting', N'Web.Migrations.Configuration',  0x1F8B0800000000000400ED1D5D73DBB8F1BD33FD0F1A3DB59D54B29DDE4D9AB17BE3D8CED5D34BE2C64EDB370F2DC1367B14A923A99C3D9DFEB23EF427F52F142401128B6F90202425F7E2910960B1D85D2C16D805F67FFFF9EFF1774FAB64F219E5459CA527D3C3D9C17482D245B68CD38793E9A6BCFFFDABE9777FFAF5AF8E2F96ABA7C9DF68BD97553DDC322D4EA68F65B97E3D9F178B47B48A8AD92A5EE45991DD97B345B69A47CB6C7E7470F0C7F9E1E11C6110530C6B3239FEB849CB7885EA7FF0BF6759BA40EB721325EFB2254A0AF21D975CD75027EFA3152AD6D1029D4CFF8EEE664DADE9E43489238CC1354AEEA793284DB3322A317EAF3F15E8BACCB3F4E17A8D3F44C9CDF31AE17AF751522082F7EBAEBAED100E8EAA21CCBB8614D4625394D9CA11E0E14B429339DFBC1765A72DCD30D52E3075CBE76AD435E54EA66751F17899FE132D1AF87C8FAFCF92BCAACD527706DABC98E09217AD001CCE8E6607B3831793B34D526E727492A24D9947C98BC9D5E62E89177F41CF37D98F283D493749C2A28691C365E003FE7495676B9497CF1FD13D41F872399DCC61BB39DFB06DC6B46946F1FD26C6BFDFE3BEA3BB04B58C9F6B9B5F4579192F36499417140E16223C0FA69377D1D30F287D281F4FA6F8E774F2367E424BFA8500FF94C678DAE04665BE31F675BACAF00CA0DD9CA345BC8A92E9E42AC7BFC84C7C359D5C2FA20AFB23E7A19C47256A81E3DF3778AE390339CB116EBA7CF3EC40D5E379277A06812CD143963F5BCB6253FD2B10C3EAEFE8F2372A6F3FE44B949FA3328A134BF6322DBE020ED7A3BD1DAAADF26CB9599443C1FC7513D5ECA3402ED3F2A5BBBEC1B2579EC7C56254A546467C95C70B240CDBBFB86328EFA3CFF1432DB5320E4E271F515297168FF1BAB1421A49BE65E419AF256FF36CF5314B5ACEB3A5B73751FE802AA265CA2AD7D9265F3860470825C58F8A8D0A4359B980A3B4524F2C6B76AA49D940BF859539824AEAB4D800B2CA2AD2B1F5D7742E3AEE6BD16EEF37ABBB8A32CA65ECE89B03AB65AC4FE75EAC9F8BA735B67CD1D20BB01AAD0A868785BD87559B4A961B03C2E523CACF1EABB9518CA6D01B668DBD6CDC444F1F591E8E328AF75989C6DF355CA69F33ACB38C936B7B36A271D16C971C1F6BA754C9CB972EEB55A9992E1AFCDA1A3C6A74A6C9B1A2A5EE083DAF505AEAE806AADC92358959D2C562714597D4912DE8D62B634B25ABB591D4FE0A56C720BBBB6B2C1B6B0CFA74B9CC5131BE5E7A132749C0EE2E56F5CE72E45EAE1EB334C0463C4BCB6851EEA4327798EC8CF6B09EF24C9BAF60E28F7CE2E7E74CA1E1C95030E176DB3D563E7E75D6AD8E8E6BB40EC55B7E1D17B0E46AA89668BEDAC0559A20EE32657F99AC3B713CAFDAD8F5C0854CD8C12879DAE11A10460962907D93E1D911A55B5752BA8D43377585AD0357A4524F01B60FEE1A4A8DAB5491F5D350F42CD34E4335B5BF020DE5691FE1AA2C50B1C8E375E3441E7B135356FED8F4E132FD8C85A976150E71517C44F5DA7E95C59D62EFEFEC187DF437D15385D2602D47FCA6BB66D275EE5F8D8FA2AB24FA27DA81094A88AFE0AA318DC754BD5C282A34871D56318E0C2DAE8DABA3D5A502AEA05CE5EE819586D99DC09DE3A2DAEB265F817EBFB8BFAF026F3EA38A577EEC45CCAFC7A84084E82399B658EA50316E179E3CDFDE0D40B3D7D56D1A2A54867CAEF69A86974B547FAAE0EBA6E1BB36F0ECB458BF47E58C369C3520DFE618DCCF59FEE38C85F86262DDAE9BCBF54CB699CB2F0FEFEE5FBEFAE6DB68F9F2DB3FA097DF849FD792B5FFF0E8D5186697C1D83BFAE65B2FBD2A45FB538172F9FAC2F2FB9654EBC45A2C15845A52C58B4857A0FC8B3585BAFBA25D612A8AB7B46A35A03E338176117A36507CC7EDD75AE24ED76BCCBC5AB42A8A589A335CABFDB268C2F1DAE4EAB1557D16BD9C65E97D9CAF869F2C5D45458167FEF2CF51F138FE2E192D36391648BC5B5EADC3F8C402851F307D7963CDCDCFD9DB685166F9455AB51A0CEF876CF163B6292FD23A4CE853B910AD744B005ED0395DE07D60F1160B335A9EB127D83D0F3AB062DAB6E9719644F14A6E7B702AF49656EDEC0F790DC106515473DD8DFF903DC4A91DAAB4AA1AD5A686115552CD15D50A981DA6A4A61AD1BA8211CFA69637CBAEE6907FD3AE06BBFBB6DDB0C55BA50B18325E630D89BE4729CAAB1DF2555496284F3B0ED8E88D6D180B35FB82F8A0EA9EFE16251BDF5DF59A0DB512F03F1B6AB0BB3F1B6A34F1E7CF71EDFDB7D8F0D0CA18BC557DF95ECA3CE738CC424F0730CCD09D87D101D6D3A542E71A95658D85D5068D69117A73D657DE20CD7B1D8ABED914718A6DC820618A81C306834745125A9210BF40DB2612237E95A3FBF8E90B8B48EF5C18410647BB0B34BAEBA8769D04195ADD57A0719D2EFF89156EA081359D85BA21512F2428FF88F03A90061A21EC34946C6EAA0D5DE091C24E4719A9CA80382D8A6C11D7D60108B0053E7F88FC45BA9C986FAA34C8B7B749F000B01111579B658C05B68A049A68C1B69E7A0E2C8DC486C07F2700C73607CA2B6B3EAA8E3F0B6CC5C469291A2871BA88D751621C1ED7D272F35A71A1ED832F39476B94561B0E23116C3AEFA2A44514DA9E38ABCB44A3E339232C363244A300F57C1602020D92233257055122346D60228479309B8902394066380C82890B474B9B7ED9E8DAADC88A2CB45DC55F6D9C3BCB65704DC35E7EB437C89C355A2F01D2A010408C3404DE03B5C3871F1BD8AC533DED850167E1E9A17EFC8ACE16B48F82A0FBA47FF8A8703397E521E24611D2593EA61B32BD55DC4079920D35A858C9C861275DDD45AFED48171FE2AB64BD32DE97613A0DA873504AAA2861066C177E3C8E5A52A01042801444B5E91A44946F5576ACF662FA606CA30C69B592F6A99C903B32DD20038A53DF5D191BD0BB4D8982E1B906B62B6275058922B1D0CEAA491192DF4362878894148B70222525F25E8894F2F12AED1E5DFB92554F8D62D1855ABAA4D2EBF958408351A813020DDD1DA4AD7B996E2B222789C55649822E30BB9301783DC07E61D4C4744BA07791DAA3AC8DEAA106102E35256C3A97076607922645749D8AE7A650BB8EEF42BCB4BD601902F514C24582C546912EFDB00348989E243608286F0A6C43CA4860A4AD00F05192A348191763A9903212841544CAE0B0B7206590247B27654D50AB2DFFB908D751640CC6C7865F25B563DE8280017AEC987C351EE33AAA274E512ECAD8F95D55889E64AF56603C49B45941FCD1BC8854C0AF51D91EB7308FF017D349E7AE6E4FA9C0CBFE82C4F1E09AD39BCB12ADE4D0E899970110BCFC2EC0013B141B504A20C6E6E4A45C06A0F52B1841C0974F2480C0B1B21D380D2033886647A16253BBF73782C13B120310B2C5334022F7000420708F6000C22B331DC04EE11980925BA70220413B3B2047AF9268B12376A503587AED430B96181216A326E1A9328020DE9503C5A8356E12724F5A3015D52FB4F2FAD62A0CA61D4D37FB05C56D15F8C2036A71E76367E1A0AD09D26A10152DA43E5509F6BC4FB50F05780F2A03A3538283072E7D534F1CBE313E011E6A6A2214C030A022D69044179360246E7FB2E82442EB679721AF938A6E11311321AC64080F85E928A1761E2B0622751FF7A78BD4616C2D717D88243C5E24218FD6FB29753209FE4F760CC058D09145E5F564807116A2377A989616B357CFDEAF378436819719A9EB494D1E8D8BCADE4905C8D3198866E2C87D5296D4EEBB064B3380289663A3C3C5CDE562C97637278B15F57B104BF6148B48269393C0D64DC08C8258F21A9A681C020C1CC9EE6030515477C445C2D89C77BB9C78330323CCD010C870B0AD20121D8C772AD17D8A994AB2F35A9713DB4154E20E661554A283F14E2522A36622498E1B1D0E1C0791089E2B7A9A6CF4D2447B04D6961DCF9B7C9DE4C3F15C91D8F3F85D545FC563127D922F93EB26CBE7D9EFAFDDD360AE1A18F305A0367F60D7F6546679F480B8D2EA49FE257A1BE745791E95D15D54DD20395BAE846AD2033FC5A69D76293DD313B94937F3B459F5BB69AA4ACF293923258DDFE23156166E3D5C040C3D6DFB49957B354AA25CF290C059966C56A9FAE057DD1AE4D864C180027B78F4A16E1614FD660FA5795F9185D17CB187C03C26C882613E8BB08EE71C8B84A36B4112B819CACB97A5F4B1067E6FE193EF5CACE44ED5741C916BAE32B3ED9B2F5F24730DBB272BDEB267F7EEECD5B61E87C3DDF50A1686FAD2851A121B3606B493269C4C0DADCBADC9C2EABEDA4382F9355968B0C479A45DC89264B8EA782635DC7D9B2BC36649DFF9117466D08BB3C2E4A0058EF0C4D592F96C0F0B265864C1C11247EC9AF77F04EC9ACF6E864A2AD12ABA7B391ACC403A45801C2871E58454257045F630DB4C892CB4F6A3ABD435191145A16BBEDB43E39E98009301167D915AAA3DBFEFA9A714CE080B4DA56CB9AB769AF0C00B0B4C28B487CBBF53C382E5CB1CF45FF3B429507C2B4978810E06C951071454F3C9612ED00C746026D08F5FE89C32B8A52C67161B14D2677E69DB8F33CBFC6C983D5ADCCC4D44B8CEAA6F28AAA1ED9F0C0E95BEFE72B75F1237FC88C6AF4507D2A6F168B5050EB69760B6BA5AAC6D5E34800DFDF865CE21D6DDD8771EC97DA936F348D57257ED2390BD0B4A0953E0606F8919BA80C52516DBC386E9BA58B0B0C4ED0C453C3B71DC1F3539B9B8FD51F3D16186B157A8C11CD3DDADD6C0DBA719DB7ABC87CD57A96FDF7ED22A9A8F337325B99C80D52F163BAC6A30AB1358D86091C3BC6672388109CD7CDFD699EA1EC9BACAED6B25E620AEDA5DCCF5CDB7BD406D8921A2CF7B2073DA18F5FE0C528350AE63E4A60B58C914B75FD450E8DD52B0B22AEE9B6E8D61AAF8242B26F1F1FFEE3C32421869B9F07030C467B4118031652E5B2836B30DDC44B1252E4B0D485F03571B50E47828263B2E0605BDE029282AAFE160580A696980852994DA439624A861414B8A7BC096E0CC97391C1788396CC0C98158ECB0056813DAF0CA7387572A6520A2F352D55C581AB65629608CA309FD2C754C5A106032769F1D6191C41F0230F27D27A54819A8E92C45CDFDB46152A480A1D6342091065434DAEC1F6A98576C760C6E5FA2CE0EA286E726AB5B34A8DA5B833D6581BD5AE82E05DAD6E3EA0198DB0238F140C9B61D83A3B931E5F9286484E0AA383BEBE903F112673D2DDA6E00009F49427E6EE28A299F304206D51D57901702C8025BE0084F86222870B0D8407E0760AC8112578832146189C3BA2DCDD4005670698DBE3DC87097D770E09B34070360A0B446DF1EA42222AD11787D13EE1FF055DADEC997F6FFF6FE0189FD0797126AAA54570C6A6A14E41E027F19A0A95265B36FCC044C93E7A244AB59556176FD537286E9536D4B688577511ADFA3A26C72424D8F0E0E8FA693D3248E8AE6BA08B9E6F09A7F2AC4EADEC3E1CBEADE035AAEE67C73F7DB131594A258825004319396E6DA4088EC859B34FE6983E2DA96BC8F2BE173CECECB5C346860A69FA34A37E7BF59454FBF65015A259C59B18956976811AFA2A4E23EFE55A70B9B1EBE9A4EAAE9868BDD73B036FE00021CFF2EEBA4B2AE8911BBD3F15E54B44EB3A678CE791FE4A2B1FD3C084430622B23EDF782DE5DB0900AA0F076D265BA444F27D37FD5105E4F2EFF714B81BC98D4BF5E4F0E26FFEE9327B175440DC0A50333089BEE7A40834BEDE5764DBE082E058CA499844B02C3694761A908B87BD36F5F271E4CA645F59D8E5797C5A7BAE7D7931BCC858A2F200DF8413F3CBC2C6FF0BEC04060CC6D01C362606D6EA4C3155D0766C8DC80970D46D20BDCF583917A69AF258C390A7257C1835120CD93B91F66863C6BD03E683A6F269D2273EC2098F2E4B78340122FB10748E412810F31A51709F64BE49591F97B21F823EF4977C874676E090C5A5D299841D88496CF5F247394D39260269BF590DAFB0403476665D6DA68E2F65A4103EB2E76DFA3869B2CD27C3EFB3059ACEC173B216203FC7D184462687FFFD30A18D03FECD4C3CBE8DA90FE01D2CD46F20F50220C9C3D599AD4B98EF661CA4962F187AE26300A7FA4858F0DCD1FF7BC6F478E498309B43A923E8440772773CC69DBE1D12B2F6B89995FD5A0AB5FF5E717A633C06F7DD3BC7B035E8DE8BFFB3045992D42AC2A0F8A37F39076E14A723C459AA6707A0C643F1D450F6C9AA603B0B166BB36DE7EAF669BF42446375BAC8F7798F8F2FED6098CD6F7610FC2587D5F27501E8F4BE521FAFD492806E5F7872589C2571B1D3683E5A3EFFBA32609B71F60A0B781F6AE6A88B6DCE23A24097C0FA1926A3AD741B4DFA314E595CD73159525CAD30EBD5E8CD89DB58909C4F77148CC44E23B83EB251924618B6713C510D82ECF42AA0E5CB73682CCC2C961E679E904A3F00C7B9B726F2D5ACA08F921A6ADABBD3A78230D43EA77D35F368A5B4F1E41EFCF854CA37A77D229CD87CE7B04E9114B1037EF0B9E47FC60D8BC37803E830FA4C1F2DE01FBE4B93434DE3BE0DE18AB96074566794D9A1C3E17899839DE25512948E4C803B24D41DF3F377CE00CF056DDA99FB90B92CC5693770E64C02269CA189EB5DF20BF0E6633A3049885C813D7D5CF958EC377AB8E744FC405E1B931D99EDF49CFA53B0522044AF67BE29B1EF0DCB9A9AF4D30D86FF25B88814C00F67FF6ABDFCFDCDDF9AFCDA7D88F71BBAB065C18145C11E89EA70D2311DA14920C13BB3C940CFFBA8F0ED2409313B27240BF8D22019A2433FE4540F90CA8D895F699C6A0DCB7DA0538306E3777022EBC09BC17B8D23C62185212347941FD4B0288F191C02205FB2E0BEA50A65D1506BB1CA8433969B35FDCA69EB067D87694853AAD52102131658055E46D05BC8405F69A430CEC9100ED0A479112C3CBAADEA5C4E2B150DBA8A3401262930A579D7314F05428EB272B8DF35D252CA474146931BF223AAAC0A89EEC730869DB86C4C85E9B0B2B31C429AF901852FA254A8CEA79BE1D9718C97BCC6105665BCBD196C5C57A450A282D17F07527BAEB16D232F36C250F46B5BB74F6A1A249E75E6BCF404079F3AED3C9747997613168DC745CA2665190F81EE9A98AA4335A24EFA72E8D2559B8F93E80152D74034A653D810303ABBE54BD68E09B21B787D102ECB64406BDCD8567860FCE2725BD8072795FE0A8D5B247755FDA5E2CE0D36DBA089F9648E13785D6F0C9864FD50929D6F454D730770737104277B058D6DD69B17E8F4A79E67A55679D265776D8555177DAA50F30752C2C4D42BF420D7DB76E632576B176B0A48EBE5BB20170E99B5858DABE491D7DDFC49434F5CD46CD89BD8252597FE0615CBE2F6699E294233C809D30155955A939A6154F4E28F64CB26071CD559CB6F02DE9773E16100EC87AB0AD6E568D53E1BE9379261954DB6FA6619A49D3636832D7BA6480460FFC604E4AD72C49A2486F43D6F153EB69F6C2516EC10499097D0E11AEE2BA911A7282F6407C5BFCE57D83B2616BFD870071DEACAD71EE3EEA860C8D169035CFDF304D3AD8EC2CEB8BF41634B1D4DFA31EB4C62FE465D032B351CCB3E66901927936546B91D10B327C185BE0BEE4EC5E4200D309BFE68C9FC11C166886AE32AFE579AE861341713C2D2184CD41B6FE9C8919895066491260852BF2A9F8270A359BCD44516606199328607BA0480FE29F2864D766A6892A3BDC9824F139758417D2DBB2E379B3FD211FF0BFC24BE8C7F38F9BB4BA76DAFC778E8AF8A103718C61A6CD89580794D6B94CEF337A74C86144AB70E1F3EFB06A5C4665749A97F17D5469DAACBA715A6FD4EA5B7CD5BDE73BB4BC4C3F6CCAF5A6C44346ABBB04D81DD511A4AEFFE3B980F3F187FA319CC2C710309A717553F743FA661327CB16EFB7924B600A10D5D926B9DF59F1B2ACEE793E3CB790DED7AFF1D90022E46B8F646FD06A9D6060C587F43AFA8CFAE086C5EF07F4102D9EBB2B7F2A20664640B21F9FC7D1431EAD0A02A36B8FFFC532BC5C3DFDE9FF1E96F3E6A4040100 , N'6.2.0-61023')

