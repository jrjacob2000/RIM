﻿ALTER TABLE [dbo].[Invoices] ADD [Status] [nvarchar](max)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201803281258178_addStatusColumn_Invoice', N'Web.Migrations.Configuration',  0x1F8B0800000000000400ED3DDB72DCBA91EF5BB5FF30354FBB294723C9EB538E4B4A4A96EC13D51EDB5A8F9DDD3715350349CC99212724C7916A2B5FB60FF9A4FCC2822440A281C68D043933765E54230268341A8DEE06D08DFEC7FFFDFDEC0F4FEBD5E41BC9F2384DCEA72747C7D3094916E9324E1ECEA7DBE2FEB7AFA77FF8FDBFFECBD9BBE5FA69F2275EEF65598FB64CF2F3E963516CDECC66F9E291ACA3FC681D2FB2344FEF8BA345BA9E45CB74767A7CFCBBD9C9C98C5010530A6B3239FBBC4D8A784DAA7FE8BF9769B2209B621BAD3EA44BB2CAD9775A32AFA04E3E466B926FA205399FFE37B93BAA6B4D2717AB38A218CCC9EA7E3A8992242DA282E2F7E66B4EE64596260FF30DFD10ADBE3C6F08AD771FAD72C2F07ED356771DC2F169398459DB90835A6CF3225D7B023C79C96832939B77A2ECB4A119A5DA3B4ADDE2B91C7545B9F3E965943F5E277F268B1ABEDCE39BCB5556D616A97B04DABC98D092170D039C1C9D1E1D1F1DBF985C6E57C53623E709D91659B47A31B9D9DEADE2C57F92E72FE9AF24394FB6AB95881A458E96810FF4D34D966E48563C7F26F70CE1EBE5743283ED6672C3A699D0A61EC5CFDB98FEFE48FB8EEE56A499F899B1F94D9415F162BB8AB29CC3A14C44D7C174F2217AFA85240FC5E3F994FE9C4EDEC74F64C9BF30E05F93982E1BDAA8C8B6D6BE2ED6295D01BC9B2BB288D7D16A3AB9C9E82FB6125F4F27F34554627FEA3D94ABA8200D70FAFB0B5D6BDE402E33429B2EDF3E7B50F56CD6B29E85210BF29066CFCEBC5857FF01D8B0FC3B38FF0D3AB7D7C9B7345E10C7A965B57F80992D054C42B2DB9E6018C13E6ED777241B9C533E65CB602807114B57DB30703E158F24BB7C8CB20792BB0B6207625DC5F9C24FBA9B817E899E3E8B03EE09AECBD257A1CCE98ADE0EA0273F46DFE2874A5860A49D4E3E9355559A3FC69BDAF03B621C5AB31845E97D96AE3FA7AB967759C9ED9772AECB6949D1E279BACD161E18B1E58CE2C480DE36755AA4A4A2A65B8E955CCEB17647EB794D92E28A14515CDAC80876A0CA6D23AE5B1CD10A0AFDF05A18199D55079B6527C551D5FD01D446354EABB43F7D75ECB4DEBA741E44DEBE7BDAD0AD04598611DE255A258C00122890161F42DB749A2C6FF5E3D98BB73EEAC4EF694186DF868D6B4A75D4BC5A49DFAABC3E5A51D63F1AA5E9A5A74DCA47A870CBC4BD849B58882B6D5023A4DEAE21225A1B14686836A0C6E61DC08A2A7A62B9164B500943D64F5DD7907C9476DDE24751DD7D374FF4DFE57651F405F35FDBA89A3E0EE43A295EFA0B672AF08AC1350C1BF14D56DA937E7A36B48CB56C3BDCE518BA185149E72C366A2AE1163EE319288D05031F2957ED7BAC92B7B415E6D24A4758192727600E135541C55E62AEA1B4938863B57F00F116E8A8D2F70088E48B2CDED43719039B6CF3A2BC14481EA839447566755EDD477A7E261583DEA4712B3DBBCBE1C1474F2DFD1225DECFDB94AEB128F1BF486087F77D355868D9DEDE411824685B49959ECDC06431A454086E457712F03A34512DD045C01B71AD0571234B155C41B94E19C14ABDCE9AA0B2F111ED55931F40BEBFBBBF2F6F7FBF9172AE829CDBDCD0F97A8C72C2883E90FD48B98EE4C37611C8280F2DD21C6C42BF65A81119F85AEDB60CF966DB6D05D6B57F80C537CA3DF09CB2C78682BE582E33920F7FE0F6365EAD46ECEEDDBA3A9918B8979BC73419E1CA3E4D8A6831BCD137A86B00387C725EF2429B1F60E10FEC1BC4EFE9FCCE55D824EC9BBE6BEE2EC3DD722A1ACF7817EA79C66B42D470BC8BD7D05DC8EACE81FBAC58BFB5FACF55BA171E7C811C8ECAFED9C2ED8D52A03B5BDB09D18A08C8763EB7086D9C1B2E9EDAA5AB5C3D49453A013582C388BF84D2E31AEE12EA7A49AA4F650F2631F5A1719FBEC8371F4971C41B1ED520DF6714DC5FD3ECD72311E28B8973BB56E055E2CE45E0BD3CB9BB7FF9FAD54FD1F2E54FFF415EBE1A5FF8218BF0E4F4F510E7B6960DCDE9AB9F82F4AAE5F3AF39C970F616E7FB9655131CB69452D5674BAD1284A54B50E1D99A43DD7FD62E3155D91BAD5A0EA8CB4AE05D8CBD1A38BEC3F6EBCC71179B0D9DBC8AB54A8A389A7B52ABC332FBC69B6BDB3184ABE873E8E5324DEEE36CDDDFF8B989F29CAEFCE51FA3FC71F86328B2D8669421E745B4DE8C735E3392CF97D057B0A9F9F2D7F47DB428D2EC5D52B6EA0DEF9774F16BBA2DDE25956FE6D762A15AF98E0082A073B158903C7F4F99992C2FC54D56C79B522A98766D7A5CAEA2788DDB1E9208BDE5555BFB03AFA1D8209A6ABEBB815FD2873871439557D5A35AD7B0A2CAAAF9A25A0273C394D5D4235A55B0E259D70A66D9553314DEB4ABC0EEBF6DD74F79EB648140C6399590E46742B7C4E52EFE262A0A9225ED0CB8C88D5D180BD5F48D724C52F5F4A768B50DDD55A7D5500981F0ABA102BBFFABA142937EFE16579E840E1B1E5E998277AA8FEFA5EC6B4EC26CECE500863976E7E3C800E7E552A23327455161E1B441135A8CBD39EBCA6F90E69D0EACDF6EF338A136E42857E8235F698F7E63CF68C9AE9F47DA36B16BB69B8CDCC74FDF591850EB0335CAE07877238D6E1E55BE57A30CADEA6BA4715D2CFF4C05EE4803AB3B1B2B2CAD522424FB4CA81E48461A21EC742CDEDC961BBA91470A3B1D64A43A03E222CFD3455C5907302089470542C4DF25CB893944B046BA89DAA08853E3212E37C9B4776A0D29B4D0826C5C175A908D870604FA1B0528B53148565AEF5179DC9953AB254E0AD520899345BC8956C62149AD1C37AA25C51BF872C915D990A4DC5C1807EED2711B3DA676DFF422595736DA9CCD04A670E01510E0649C5B3CDA49E218EEF8659B62136C84751CB9B13BE360088CC53B18610F807D94000ADD0CEBA329DA096EDC95DD19471B8321806D833B06611D1D0A23708E8EA82E5D83789D9DF20E886EB14D341EEA62E52193D23206C874956DBD58091BE488EC8411C2A57B315C62971C05831F2CD3AE898450388A459A788B264DC053078EEDC3522816E3B1144AE48360296DE0B28B2903463F8CB504BAD07317CABDC7474703984F1846235B5118DD3DB8AD7D9260772CD7F8889AB75A8ABBA8C55CB6B295E2602AF212775B1D948D640C46DBBD49B4746218C1F77A371A0F0D7FD0AA24732C8438D32082C743E999DF94B39E0384E325232663683E13AD9DFA17427876C95B5649A4755D57F8A9032775904761196807E24843D04312487210817D96F188022B0B1937769680AACEF2AE273F61431D95AD3072B871571B1FB853D3C889B7CCCFA5590C2554115960F7642C0F3AC82FCFEAD0D43E43DBE1405E0F7574D1AC41608435A421E84188662400463BC38668188175404C96BB5C3604D220D0DBF09841C4B27EA863B09396122E9DE3D130237193C6A55937E736FFE676DE95201577C6B278476B988B79E80EC25DE6618FC0616692B820A00DCFDA0597316F745706905DD307E132C9B15DC365CCF375142E83C3DE019741921C1C97D59104AEF32F85150CC2633028617C2D691CF30E180CD063CFF8AB76D3A95C29636AF0A93C767557169227ECBD0D8A2773F1CD991390CC2225F039299A6B5C217F573E9DB43E42CDED37480AA6709C0CAEBE15BE2EC81A87C6EFD22D80DA37C91518CD8EC302A2DA636100D886CDA579F388010E84EFCB2CA0D8BD808E28CD0D9E150C1DB60508BBA8B141AAF712281CBEF3B28280AF3C2080C0D6D50D9C019015048BDF52F9056C336C7C27C94313C056665A80B2D70214408A80F7408E87001AB163A6A907581EAE6704CB6C118751B3B0020C20885390400992515A9042B202A1129ECE4016D556D7C56604ADF050E4BDD5595100D2E22AC737C001BA0E1E3E24AE19BFDE194FC51E75C793A9D02C711B2D50073C2B4D3B1043F11A436861F62C83E7BC3ADF32017728C20D94D07A9409C0242D198C1E70BAF434D17B4CA143417DA6FAD006F5947264BBEEE491DE34D592C7E0FE838D06770002E469D5B69D38B8BF8F23B57B0A16684398E58BDE99452B18507796BED206756071A27E5762B5CF6FE934107AAC8E9EFFC363F52EFA473E441787DE5879FD9710FE4C22B286EC0E05D81D9BC6A5000C06DA9BA69564742218463F2B4FB36949E3C01DBA1B71951C4E8418974794F7DF4C94D05FC4B9DCC4F6A70B7AFFE6CC753D24888D44F69B4AF7BBCA6E626574D228E93055AA18EF2D9D6E2E1D56BFD35DE5304B087B630EA182E522CEF52A4E1803DBEA9A08A1BF741369AA6E9F7B1345F7F88D4A18973B259F5B2561606C320C04B25C1E6988C407139C4A7C236FA7127627E2732BD28B4AD2E587864A7C30C1A9C478D44E24E448DFE350BF1789E0D97DA0C5C6A3419B63E6A6EC6C365F3C9275C43E9CCD689505D914DB6855BF17C10B3E44D51B0379DB927D99CC37D1A23C0CFEED7C3A795AAF92FC7CFA58149B37B3595E81CE8FD6CD4B2B8B743D8B96E9ECF4F8F877B39393D9BA86315B006ACB87E24D4F459A450F442A2DDFC15F92F7719617575111DD456568ECE572AD54430FD535A75ABC4BF4DC5C9D4D7EDAC59B95BFEBA6C2CB1BF0705DBD87608DDFD33196BAB61A2E010709C6F61442F9B67494212F245DA6ABED3AD15FAEE85B97FA2E5E6C69410EC180027778FC916C1114FFE60EA57E0B5B84517F718720BCE42C82113EABB0CE66D21429D7430A27482B54E62F47EE130F903A331F7E32E6C477BAA6C3B05CFD468BD8BEFEF25D4EAEE1FCDA695E359B6E8769D5B61C4E90708F36598EE83CDD0CB8C0F759005AB0C81D661B1D2D82D3C74C5BB153A51428F010775B0456F3D1638420E538182528F1A4599BFD55215C5BE40EB3C9262E426B3E0E2B0574B0E645546C259AF16F7B234BD87EBFA32441EF901CE488A6DD3052A4CE048FAC7A50E0CBBF0AB7099FDD61BD7BDA50DB8C2C5570B0C413BBFAE14705BBFAF3AEE4EF8F25496AE64A0B65ACC2F7DDEAAE03B2782CF791EEB28A399B749458BAD603CAAD20B68518E40ED6B121F85D0FAD4D012FC26ABFBA438269E04568B0C47BA46D8035325C7DF4B51EEE01AD1570FDDB71AD68EEB61DD689B6E5BEEEFB401E70602B8B055E569F9CEB5B3200E56277D830F1B7081696F8AD4075E579EACB3ABBB7A42FEB8F1E2B4C7C2E08AC31D33B42067887B4621B0F847EEB15F5B5705FB49AE6C3AC5C242B34307FD5620F2D00F343031D008B3CD6B5900D1A2C68E1FBAE34F221F13ABF0BEDCAE9F8C5AE0B93EB5AEEAB6652DE68066C2717BAC3959F9A16C1CA651EEBB9CE4E04D6F01A71EC36C1602990C182A83F79AC059EE018AC04FEF13B5D53162710C79525BAE377595FC6F6C3ACB2305743E24B28C8D6DA77B7253C62004F4DF48F1BE8A11D1E1FF6E5C0EEBC77585CD7FF4232ECF91C48D02BA3D51478EC0C944348DFF3C726032FDC97B18FDFE51AD2B991B85DEC89814C1D6EF78CCD776D26EDEAB255F1A1E939394D5058F709D283D0EEF359742AD8E96B2256F550F87B10E0E441F346C4CE264CE7EFE8344972C09DFF1C59210CB49D0E607ECBA93F156042998F9212538042352596F86CC5419E4FB81B07459E5B0FEC7A05147482A7A1285EC343BD2AF93B81AA554ADD2123993C45D0487107D808CE72998741A626FB04B6995AEC7144DA64FE9485E71E6B2AAD63B3B7AAAA2384FBE92A0D8C612461185527E44F042663FBD91316CB90A80063DFF7928BB48EDFDE5C540784F7E3220D0CBDA4011907A1A031A649D4C3BC11D3084AE7B6FA348A7A787EBCBA4383AA09D3EFC80B622CBF3F17185B0F2B0760124070540A4A767DFC3AD861319EB80F238454C5FBF88D67D2424EE078D16E1D66E4947BF8BD922FA672663D0CAA3FAE20811EE005B1C0131E862228F0B0D840223C60AC81125F88188AB0C4436FA329ED8006476B74ED01C31DAFE1316F68B23A3081688DAE3DA02C82D6185FBFC15827C4C34C09BBD53B922955BDAE43CA602ED4DBCBF5ED6627BBD994BA4B40A0236EDAB7D13ADBF446ACA87659C6D5937AD77999C8B7C968E8326239CE4D650F25DC4DAED23027FBD2FCDF84BBB1503310035791A38C68ABC890B3B03739F6ACAE329D702B922E99E7BC20EBA3B2C2D1FC2FAB4BBA7CCA5D2BAFF0214AE27B9217756EE5E9E9F1C9E97472B18AA3BC8E4E6451756FE4D7DF9CC2EC4E5E96617664B99EC9CDFD83F54A2879BE040B40CD486D885273CB12DD2543749B1D7A9BC47FD992B8DA6ADCC7A56CF2CD2E2EC6B5D530936F51A9BAB37F5B474FFF2E02744ADCCAEE926A504BB288D7D1AA9C7DFAAB4ABB3D3D794D250E5D67B4F8D41BDDFA9A8901A7BF8BB8B4603D810897279DA8E89CAE5C93F9EF10F8A2DE1A046088D1888D277839045A8B779E3A90CA6398D7C9923C9D4FFFB782F16672FD3FB72D9817934A9BBC991C4FFEE68D0D9A0BBC170BB4AAADC7E8389010630B22459A80B49E70600489BFDC749E81D6297D984E9A489361C0F715242A441ED1E6CDE2CE3209CB1A7C081209849741F29816EB75FEB5EAF9CDE40BA559B93E85BCDEA7AF8EBBE1116495C198B4BE4BB68D48B3F0CE7EC97F2751D1531C79CC6B0F79E4D44B0F81E4B14AEA78B8006A32BCE21DCDF6D286981D8EB4DB0BFB44F4A1EF23091A30BDB069E3E26A5CAA001D4F18301A6EA085782347C7F5A71D87D547948EB6FC6ED074CE87B0F49CB6996EE6B818F316407622D16EDD57018C71EBB79A828CAE8972AB61DDC5FEB880E0B61E0B4E80D34B5A8DBDDAB054D787B0E490F0B49EE6B0149836909017A3D586D5237BA27EC763683453E221F072B0534AC5CD20004CD923220048E6171B00120B4E0BB1C9E0016A87B461315C711E04E30F7CCD22468FF510842D983E66B4187CD6EB788483D9A954DED1F6B3C721261AA1F6CF5512E83272BF2E7F40BC5CCF91391D90BA0880266CAEC76665B45B486D20DA182BA63DA4170EDE4F4E5F0731ACEC7C580EBAFC557D7E61BB0EF82934CDDB9C657A44FFD66552B40912D5AA784C997D0E7917BE24A74BBF6E0A977DCFE9E7A3E8804DDDB40736CED36E0C573BA8D5869AF5A6D5E2BC5710C2B3BA0B4F18EC1662B70543DD426D6702DE9CE0116EDD49A8C6B475878504B1E935B5CB60E5E0B5EEA821D16A3D0E3B9B38355F31C45BEE500F2171636388A48ACE953FEFCF849A83A5CD73131505C99216BD4E13B13FBA4988630B71E22004B20DE7078224180D6CA258E2C2901637A6B82F6723C8CE9C1266815527184560D8BBE47B67D6D20698F5316D7DEDD5DE3B751891B69F87AF839C11E30168E1BC497850CC5EFAA7C89167014106C412849D858217103F1875160C60483F2434D62C38E090738E46960507DC19639D7A106285E0E9329A7745CE8CC81169B33452ADB55D1571B9D1A6BD51C5A58C4F4927088034DF2098DF2860A8462259A93EA272CF9A175914AB417437599C2CE24DB402484BB51CADEA92860D3CB9E48A6C48526A3369682E5D9942D71AB892B6B58D1E048139CCB73E4F7AD82917FDFE1440FCFB614FBDF9EDFDBD9B7EEE7BA0093D6AA7AECD2F2FCC5BFBD19D07B8F39908A7F936C8DC1B927B059F7ED4B54ED395F1B9EF51675F9F74A2DBC4EDA70CF0999B91A5C08DE131EC3139A17663C5FD43837302708C4360B18243E705BDFFDFBE3283681318BC177BCEE4F1D1D15ECB09F709DB8DB0D02777198F49F0ECE320BD3C4B602E3205FFE6CF0F76F333140F681FDF1F66FADD26DEF0D8F1385A427483BBC523A83B6DF02C730FDDEF202F81924178C16737D75757585EA6473A343CDC3E264F8494040EAC8031C1E18B02FDB3F0FB2B0CE4E781024CDCFE8A029F091A5D1498B22E8C6A1238F183A33EDF6BCD30DAA9923F33ECF85C89ABA591B4C2F867C9236A051FFB63E75A417468BD451EA817274DF47D0533070A3CB84071EC4480B685C3188BE6C414E199C39E6BC1D5EB74240E91FC306FB107BC854995DD36C53955CABAF14AED7CA56316563A08B7D893300CCA30BA17CF3D5C9A77C131D863DDE3720C73CAD2700C2BFD1E3946F7BAF99E730C92CE665C86D9953ADA31BB386BA411B9E51D7CFD945FC6C92F952ADCC2DEBB6D2EEFC4873C27AD7B4573350ACAEB774FCFA7CBBB94B241EDA601AA284F95233DF2CB56A4335E84F75395C6C4DE4763332B5D3425580F8DBB880D3EDBE229D0D9770C7655E408996FF670F8BC54DB4BF3C2B1A5AFE6AE4BE9A729C1FA6085CEF0D9AD89AE13566CE8A9AAE1D01DDF6FA93DF112B4139EDFD60E1FECC4915E4039DE177C82DAAD477D5FC65E1C9609D8A0A86B0514633D5DE49B8FA4A8B5926B67ADA6D076D856D177DA6677B375ACA83EA55FA586B95BBFB132BBDB385856C7DC2DDB60F8F4CD2C3863DFAC8EB96F66AADAFA16BDB2D55E4129D61FC85B22F705DF03C7DDFC26422551646A1C01B183BC6648CD17459723E73542ABE69BEC5B0E07E03A38E0D3A61B9FDEF1ADF7101185D4B6E4DF7B0F55F1DF42466AF6F10248CB36468571FBD1305C4927D68901F8B760C3846A5A3F54BD435357A47737B7D027473F6883EF4E9041635689D89815045DBD10B479119BDC36FA0E6307B30FFD2BB4025A730E8D1DB18B63E5DF6CC3B40BBD2E8C8DBA11609C6DF737E8AA564CB7488050A024D4D04DF36ABC500F32B392790CF28A871C22B4D94D23D5DF1876457C47F38B67CCD12F5EC781773140C61BB47CD9878CD7781F1884A9873529916B2D6C9496CB2FC3F597883528300D59B333C43368F72782E6E6062184CB1D8FF90856188952E64812B081D4646A0D4F14BEE3B313459B737448A2809DAD26F16878A2B003073B4D9083FAC1491272E928C9B59AB2B359BD73671FE8BF4A12ADB3D9E76D52BEC851FF7745F2F8A10551E6054BEAC3E21628AF739DDCA7FC545DC2885791220B3F50E9BF8C8AE8222BE2FBA8B47BD3F2318EEA8CA17AE0A07C12E68E2CAF934FDB62B32DE890C9FA6E057681E5E9BCA9FFB39982F3D9A7EACDE53CC410289A71F988C9A7E4ED365E2D1BBCDF23F1F11A10E5B13F7BFAA29CCBA27C02E3E1B981F4B17AF5D20510235F735BF185AC372B0A2CFF94CCA36FA40B6E94FD7E210FD1E2B97D0D4107C43E1190EC675771F49045EB9CC168DBD37F290F2FD74FBFFF7F02E1264DED280100 , N'6.2.0-61023')
