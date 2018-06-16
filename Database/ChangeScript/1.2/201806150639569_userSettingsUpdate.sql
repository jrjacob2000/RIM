﻿ALTER TABLE [dbo].[UserSettings] ADD [BillPrefix] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [BillNumber] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [CreditNotePrefix] [nvarchar](max)
ALTER TABLE [dbo].[UserSettings] ADD [CreditNoteNumber] [nvarchar](max)
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserSettings')
AND col_name(parent_object_id, parent_column_id) = 'CustomerReturnPrefix';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[UserSettings] DROP COLUMN [CustomerReturnPrefix]
DECLARE @var1 nvarchar(128)
SELECT @var1 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserSettings')
AND col_name(parent_object_id, parent_column_id) = 'CustomerReturnNumber';
IF @var1 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [' + @var1 + ']')
ALTER TABLE [dbo].[UserSettings] DROP COLUMN [CustomerReturnNumber]
DECLARE @var2 nvarchar(128)
SELECT @var2 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserSettings')
AND col_name(parent_object_id, parent_column_id) = 'SupplierReturnPrefix';
IF @var2 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [' + @var2 + ']')
ALTER TABLE [dbo].[UserSettings] DROP COLUMN [SupplierReturnPrefix]
DECLARE @var3 nvarchar(128)
SELECT @var3 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserSettings')
AND col_name(parent_object_id, parent_column_id) = 'SupplierReturnNumber';
IF @var3 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [' + @var3 + ']')
ALTER TABLE [dbo].[UserSettings] DROP COLUMN [SupplierReturnNumber]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201806150639569_userSettingsUpdate', N'Web.Migrations.Configuration',  0x1F8B0800000000000400ED3DCB72DC3892F78DD87FA8A8D3EE844725C9DB1D1E873413B624F728C68F1AC9EEDD9B83664112BBABC81A92E5966363BE6C0FFB49FB0B0B92008947E245826095D517BB048089CC442291486402FFF73FFF7BF697C7CD7AF615E54592A5E7F393A3E3F90CA571B64AD2FBF3F9AEBCFBE38BF95FFEFCAFFF7276B5DA3CCE7EA6ED9E57EDF09769713E7F28CBEDCBC5A2881FD0262A8E36499C674576571EC5D96611ADB2C5E9F1F19F1627270B8441CC31ACD9ECEC669796C906D57FE03F2FB23446DB7217ADDF652BB42E4839AEB9ADA1CEDE471B546CA3189DCFFF137D396A5ACD67AFD6498431B845EBBBF92C4AD3AC8C4A8CDFCB4F05BA2DF32CBDBFDDE28268FDF1DB16E17677D1BA4004EF975D735B128E4F2B1216DD871454BC2BCA6CE308F0E439E1C942FCBC1767E72DCF30D7AE3077CB6F15D535E7CEE71751F1709DFE82E206BED8E3CB8B755EB566B97BC47DF36C866B9EB5027072747A747C74FC6C76B15B97BB1C9DA76857E6D1FAD96CB9FBB24EE2BFA16F1FB35F517A9EEED66B16358C1CAEE30A70D132CFB6282FBFDDA03B82F0F56A3E5BF0DF2DC40FDBCF986F1A2A7EDA25F8F77BDC77F4658DDA815F683F5F467999C4BB759417140E16223C0FE6B377D1E35B94DE970FE773FC733E7B933CA2152D21C03FA5099E36F8A332DF19FB7AB5C9F00CA0DD5CA238D944EBF96C99E35F6426BE98CF6EE3A8C2FED49994CBA8442D70FCFB239E6BCE402E72843F5DBDFEE6C0D5B345277A06812CD17D967FB396C5A6F91310C3EADFD1E56FDCB1CDD12A29AFD3AF591223DB0166BF7902A3DCD0FB792014C2AFA160BE576D84A1BC8FBE26F7B5D88103309FDDA0755D5D3C24DBC692E065F1336DF826CF3637D95A946F52FFF936DBE595B47FCC348D3E46F93D2AED916CE79011CBB6A50A4DFABF1E4FFA3F84A8E3FC779AF84F60C657E6458AF2A173B5E1D7FBDDE60BCA475F263EE42B5F187B5102973BE4058E4A6F8EA2820428B77832ECC6B7302BF0FE3BE9A5A9782558B03A4AA86AF50ED54E623DD55EB668D5020C22D588368012572121C4D7BAA24374008810D50F9FD26D94ACC834CF4AC421A76C2421AA6EE98EF4B70D4ACB4B544649B5E90671679A002B26540F600C348290B55E88DC4CD0A7637C7A5A8A08C30E692D2228EFD562F4A17C40F9C543351B0A7B1BDC8259974911BB19F68625257ABC61091E08EEF7E5D4B43B8195ADC3722A1AFBAAE5D6D7724AE003EB29AD512CA86DF5782B2A841BDCC2B0960EC0D571210536756003C3522A6EFD7AADA564E8AD56D2BAED1358476B3A8DCBDFE90FC756BAA14FE75E16A0ABC72D8AB126F6B39A556879D2969ECC9A3196DF5E83E5BC1E3BF6E2BC40F792F7664F32F601C9EA17AC692A257683A2A23AB31AB9C3B0C6EC38DE4C784DB1DCE98AB602BC0F76F407E830B23016609CA475DAC97CD12DBF4C83CF64C11370632B615B866BE1D39C6920B62D44A651ED07F38CD6BAB2CCC266A11DF00D65F4D87A25965CA3415E6866305CCC96E68BA762BC0CDD4FE33F57BB78F039D6DF77513D7C14C8755A3E775F9EB0062E475F6309C5CB3C98FFB8EF6ECC5E8F819311D474D66AA3E112BCC72132C36B63668B03D4CB3B1CA891B3B665C6D2C847BE31CC4E4E38745CE51A0E52732DA7AD541C69FD04D49BA7300A579F202AE23CD936515623DB90B76515B094DE637308AF99752CCD10ED79836A015D6649A73DFBEBE1F11D72D1638512EDE77586E75894BA1F4E92C0220F879C7E0DFA363E4AA341BB46B2F66C0913D590D4C0BB15DD4BC1ABD00457813E0A5E8B6BA3885B5D2AE1CAD5AB1623BED1206F1BBFD8B8A8F6FA9327A0DFAFEEEEAAC8D4AFA81A2B2F9EAB251EAF87A84084E923D98F58EA50316E179E8C72DF2ACDC226749B860A9501CFD57ED3906EB6ED6660D3FA094CBE2031AAB7583CB618F4ABD52A47C5F82EC7D7C97A1DB0BBAB4DED9918B997E543968E3F5417595A46F1F8469F7795D4F89BB4677190C74DA8529DBF493E395BBC801820FF114392FA34C616B9A1AF7501F73E9BD563AD750D3BA8FD7AF8ACD57EDDFA09A87DDC39CA511A8FAF500E2122DD531455D53FD16B83511A274A45F271AC11836CEF9DB7775DAE8B04B156E5B27E8155FD68411FADF251057D508CF4411FB4D5B06D280BD24D1D3E997394915555EF507528BDC8A4D01A91F1A0D0066BB2704B4D98788680E94BBD82B1156A0F8ED8F69114D033D24D8FA72E7DC94249FBD4CE7A44451DDE2FBC7D85EAA2AA039D727ED7E68DBF2AB6EF5179443F3C6A40BEC931B8DFB2FCD72316E2B399F5779D82AFD5BB8D827F7EF2E5EEF98B1F7E8C56CF7FFC0FF4FC87F0CA1E98EA27A72FC6381432784B4E7FF8D14BAFEAED58817245283133DE9F4933269A58AA95038AE5265E44BA02E55FAC29D4FD17ED0A5359BCC1A615417D6602ED22F46CA0F88EDBAFB5C4BDDA6EF1E0D5A25571C4D2C815BE3A2C3337DC589B7C9CB6AACFA2978B2CBD4BF2CDF07DE9322A0A3CF3577F8D8A87F17DDC28DEE558206FCB68B30DE30C0E14E1CAF4E56D683EFE96BD89E232CBAFD2EAABC1F0DE66F1AFD9AEBC4AEBD0F74F652C6F5B2C017841E7551CA3A278838519AD2ED84D65CF300CAC98A6363D2ED651B2816D0F41857EA64D3BFB036E21D9208A66AE7B80B7D97D92DAA14A9BAA516D5A185125CD5C51AD80D9614A5AAA11AD1B18F16C5A79B3ECEA11F26FDAD560F7DFB61BB678AB7401C3C65BAC21D14F284579E57258466589F2B41B011BBD3185B1500F5F100F76DDD3CFD17AE7BBAB5EB3A15602FE67430D76FF67438D262EFE9AD461CA161B1EDA1883B76A0FEFA5CC734EC02CF474E0C80CDD79181D603D5D2A746E5159D658586DD0982F426FCEFACA1BCFF35EAEF7D7BB2249B10D19243E2770BC4CF07020C24B12DB1268DB443CD8CB1CDD258FDF59D2631760198438DA5D20EA6EA33AB0330869755F81E86A32710311D6741688B24A8105A2ABEA2A5C6A3109DC0A445BD7E12814AA8C84574591C5496D010847E534D599C7FA2A5DCD0CB7A434683377AC60E4B19190549B618CC1F9FCF8E8E84462871A701BA1D101A6C7B13CDC3F4840C9F9777579355E800A6CA0246929DB1E491A27DB68AD274CF8CC72535A71BEED40ACB9445B94561B093DE5563D33010E32066D47823165E2CFD982910FBDD8F0F9F1AAB15524CB77234BF22CF981D5498BE2A2B940B202D213405240AA6DFAEDF2952794923650553FA872B4EA6039912F27326A2BAF922292144C5444C20F4556B8F46BEDD8C2B9D882C4D0183CD310EB6003A263298DFD0507422094EC408C3D00F1598AE99DAA11161B42C2D32653D90B8E04165A9BDAD4D3514447854200C95131D5A66B2E9B7852D9E1726F4D030D27E21A6548B76881A055FAC756B70D122588C880E20431C2A67B3699734A89E253330DC3AEC8D394248AE4C13AAB26453A760F891D22522016E1440A64F2418894F25A151B5386A37E1C6B89EB422D5DA0F4822E82C1E6138451602B0AE2BB83B47517264DA3C5840C20A58251A50331A34F43C01DD4962A5F94854A5293C6D1583002219415CC502BC96112ECA6141A3E93C230C68AB40A497CDC3594FE127D93DBC89F5ED2E1114EA0403E5BD9E85D6AD41E081575DDD80DBA9405E35DACA41BA5FBF9C43D0896804970C912787D10AE6C3857C96EE4A5C425EFB22525AE1AD7D451044B4023B860098CB65B09BBCCCC89044B75D5847AE88DF74EF4307E6CE0873E4931521A44C20CDC38107B0BBC14C44E0474672E83E56BBAD3173D91C145ABCF69CCE472C5DF9CADF500E8367EE05989D1A7D063D3E7D98730C1AE0FE4E541C90A776B876980E1ABCD0D92630826D1DE88DED33873E003F808AA0A57FD8BA8E23A6CAD300D1D582FF49E26930E8900734AC7E443D8F8C20FD6DA8DB866E3EB49A6DC37BEA34855F85DAF96D107B1EB056E31508DB9EE4A0366C0B98B35EC6D46CD6D0800F4EE8E8371820195A406102A35276C3A87AF3408244D8ABC54D5989B9254BB71976E1AB0172C438AAB42B8489AE528D2A5273B8084E959628380F28E8D29A48CA414DB0A80985F3C8A9409D9C90A2923E98B41A48C277B0229E359727052D6A483DB8EBF901B3E8A8CF199E5E157492DCD130818C78F3D93AFAB7A40EA7CB804EFD16519BBFC5255A247E8E65C8C27C9D32C489687282215F05B54B6A174C5C375FA4B75C77F56DDB570D52681B411884C0340E244704D64DE75893630341ACF6802C46F3764407CBD15341021B2933500E86E4E9620D8E2503B1A2000C46B61F3797BBD290C843A270CA048A8886A8CDAA02E3B30F4D50F151C12BE6302D638A44038D47B6704416F19074090732F3B106A360B5E200338722D882C32DCC6C7247A8286D601ECB4B80128B9844E02242D390EC8D19B65B4D81163D9012CBD05460B9658471654936C75082097FE2E806274B5A415BA473F9956BC7680F39EF85DAD225DAE2582D542D23264CE8E63E0B4F88AB9F33C95161C101E3D95E9D7647D016E580DED547D6A288733BD46A4BB1B0F15E1F0E11A80B574ACD68774E9F8CC4276FA12CF3F7BA8A05F9D9C23630FA6E7885C6835B3891760428E91A73D9821659100BCD0679A70C82B734D18DCF9F55BC3096586093B27788BCD1B3FF8E152F3449D41019202E6500CE10D9839612976FDD923BCC0A4648F261D00A2064E0890D9436D36337FE014004B860FD42DBC01A95731EAF876A56E0023DC872A1C30A6DD7600FAC893F8E402204ABA986C7ECC1551D92CFEAD91AD931D451C360787DAFBBE58205CC4AEE48326CC1822020E349639622328DAD0E251EC14C5F5EF26E680A7861A52C473431FEC114F0AC7B16514D7CE9B1804C67C6A8811A33E7D30488CF3B498A5BD18A47CFD0A629255FCA240942982D1426D58421C79AEC16F6D99D864B1473084E00D6350C85D836E9DD2848F017B1DCD1A65BD5F0AB33E81A14E1AFAD5215120096050543F6E803150D6AAAA076BC0E8278035E628298E1E6D9C9434F7150E5C33C451B5091CC663E48D79E5D647FC78E04E98851B7A530370BF19625678CF993A6A85A181F860751E38757C0ACB0BD9AF3B9829AACBBE65C6D8845F684F1485000C863032181A0619E22C144CA2C478E712F5309BB904850FB804100CE2921027A0E01225C63B97888C9A99049C7E3B9C7F0F62117FCCED69B2D19BF1DA13D9B6EE6C711B3FA04D440ACE16B8498CB6E52E5A37F7E3D28A77517DA76AD17D494A66B7DB28AE34E81F6FE7B3C7CD3A2DCEE70F65B97DB9581435E8E268D3DE2C1D679B45B4CA16A7C7C77F5A9C9C2C360D8C45CC715B3C3F6E7B2AB33CBA47426DF5A8F00ABD49F2A2BC8CCAE84B545D1378B1DA48CDC0F367C5710BED123C629647931EC3D0CFAADFCDA7CC4DC3FC39B47C644F3E7E8369AC6C949A5CC4F939B5DF6308D5DB81512E5EDDBAC2ECC8D6BB4DAA8E43507F5D599149BCC315050F86ABB087471F416441D1327B28CDCB812C8CA6C41E02F3CC1E0B862996619D2D842192222924491066A8285F96D2C7FAB77B0B1FECB8B7923BD5A7E3885CF52FFF7D53F27D0E2E67A2F61D5C1D109B11D67F3FCE30335906C220A9920F34983091E51C469A887335B4DF75544F31EEAF9DC0FDB0B5E4865C0C69CE99B816AA72D1B4C35BB9F5C855C8F21CA035F610BBEB085968EA4B0A4DB8C922CC963B4C851D0266032D9C6A9AF7995E2A58B76554EE040B8996D94369DEB4616134257B33CD358E5EAB79DE7F890ABC38F99DE9C2EB0980E84E39D7090AF20CE52AC2CFF60FE503CA2F1EAAADB030B3F81A479E5D26452CAFEF4295C39C8D1E6F246ADBC2DFB551533F8A3622E7023D751118D265A18914DF8DA387EACE20BDC155B8CE00495E99627B58578F5B1463D994C1F1358ED8C962C7144FA5C19F962E6A84AB39229785AE2977D84ED54FB55473E90645459582C16DACA4DA69D7D503DA7419020FEDB5204906E8A90B555F8FA811BDD83DECEDB69C86D0DC7AAB86F6F75D54F38587D595DA43FA94E21D16A818F81A674ABB9B550172D5D7AEAAE11ED05CE1823C7BCE154510ABC53C517EB9AF1ED44B54C479B26D32E6383B9EAD70B248F32A2B05AB65CC9F3AC85C304EC56A7BD837A89EFDCB2C11E70B5FE33603E599E7B81257AF314A2B7153E830C3D87702B839A67B404003EFE0662C89331E3665C1A06AFB79ABF87C9CC97B7577571DF17D45D55BDC806D2D573B2C04EDC38A75400CB70CF0550E531BD5CF194A00D9F2A916E54312771AAED757D2E1A8431B21577DB9AF8B93F4C02B277662A53D5CF19D5A16AC58E7309F3775A22D37873740EEAD0EC6F2214BC5F9DA1439CC85E6795A6126D0C2EF744E9120F2DE730A8C91B79A538A2FC79953E4A606510533C58777FAE9D78153F54E445446ABAD18C339AA36B1D748C686167ECFF371A0CB82BFC1A0F7DC0CEBB6F033ABBC1F807A8BC0602F1EE767ABFA427235B4E1DAC3A746043CA1CE4ED0039AA5AAE857BB135EF662901EC7BCDACFA73648A73A7597427F070E4E7BC94AFF015283503A55C8FD539C5B457127951A0ABDF1919BD48A5B20271B30559A86D5208917D8B88F9111C2488E0B0F1B9DFA03BC29B94BF28D681B89752E8B5351FC96E5ABBF46C583B83CB1352E4E8F789757FC2EA3CD56F47B70558E9B3CE82C8BABE8054FC151B8858311FC5BF606EF1EB3FC2AAD1C9B0274B9D61EF2DB2CFE35DB9557697D86FCA98C79D040750FD800CE629D838117C7A828DE601145AB0BC0D693AB1DFCD17816CBAB5457BA378A0F4876F2B05435376E0D5BAB1430C6D1847E96BA1A65799FC9143BC2FA395AEF2060A47C2FA54899AFE62C45CD056BC3A4480143AD6970735CF63559893A5DA8727290D7DFFC0D095B09AE622C599DD0A0BAA5D7DEF59405F66E3C7729D07E3DAE1E78BD2B92142F1EF20AC0D74CEDE81ECD2D4FA824BE6AC84252347176AC2C7374973C82BE155A356D7452774A27232AD6B943058D4FA1CE411EA2FA00504694AB708407A1C855B8C6A54108F235AE102114F91AB7990A61C896BB4103A70F53EE9CBF9295A034CAB57D206BB26398DAF0EB149F6A0D84E549B7A5A8A3EFA4A64E0EEB2A971C0C9183EF5591396565FFAAE3EB2A4EB508F4C44D798B796FDB5C8B155E25B0005597DF5F63B5B15E9FCFEFA27501051D03148B69F6B27848D9F6629356384949FB779B6D4F32DDB914FC9A1D55427DCD868264DD8BA9EF4D93F98C5A83583B7E2B4AB439AA1A1CDDFE637DB14EAA83C9B6C1BB284DEE50517ECC7E45E9F9FCF4F8E4743E7BB54EA2A2B91C8124F5BF14EF69B7CAF23F795E65F9A3D566217EEE7E574005A52856DC04606CBF36604A99247F862D635114A888E8AEE93F5B881F9E0112D9DCC2BC4B937FEC50526F19EE924A3755025639135A215B68417169F50DCCF46B542DC1F9BF6DA2C77F6701D697E31BE0D133A606D40AC5C9265A57A38F7F15F5309EBCC01A07CF335C7DEA8C6E732C4380E3DF655259A28E409843905E5C6495B94138A04CF683908BC6C4F72010E198AD4E2A3F088E33C7A02A88D2DB15D7E90A3D9ECFFFBB06F17276FD5F9F5B28CF66F58AF272763CFBA7332EECF9EE00643A3083B0F95DA748627E80F2CDC6D10C90A90ECC2099E253DE3DE8B9CE7E1B401C05E281342F52DD66D10E84134C9F845891648834D7D58318358E766740D6EAE370D7C7FDD21F8247EDBB52205C1EFE9E68103EEDD5DD12B01E812EDF6D9C4EDAF4D871C03F69E506A4CA1F846AE392EB79F6E866FD75F1A9EEF9E5EC23E65935D1DF458F6F517A5F3E60C9F9E1B81F1E5EA62B9F913F74EE77F9F806D9D9AF85C44AE70CD46B0EE33A40B159F53240B339CC922610D687A74ABA08C00350FF6641B08DA532B7FE7054E85E584F4B267370887A69C10CC2A6BB10A0C1A5CE4C7684C15F0330D2EC5E8AD7020CE71D8535E566D17AFA2DA194FD83987A56AE62BBCD029BECEF41770269FEFD67019FDC3F6C36F9B1A6697A7F03EB4BE28E0B97D53FC4EBDCC119EABA0A3ADB8044FB8398724052FE401B5B48C71F49C9B339FAE3AE237BB2FC86136828A9FE2064D9DB49A314F2E701A6189DE80124C951F10089A4E4FBD864D0B4FC43DAB08039EF0721F24C56E81309B9D82FE73D97833F90B29E3E4DC8F825A9F803CCB9C053EF60BD05234F997D3BEE0C115762B56031C9F983B4000533580B0C9EFE7E55B93FEFA6FF73A0201EB62127DBCA54FE102AA93BAF61CE604E4E5F783187CDACAD88AE7ED5C5CF4C27433FFA5A07A4EC7C0DA2FFEC3328AAEC25A0299C956F1E43DA852BCBB134379FF2B23C70F829153DB0693E1D808DF5B06B13FE0F6AB6819B31DD6CB1DEE13109EEFD0D3AFEBA001F7B64FEB2005F9B508FE75DF01D01FD5928DF0AD01F16700D80DA7CB021564CFFEF8F1A90EF3FC045DD66FABBAA21FAE584EB1090791F4225D57CAE33A97E42788B5A195FCBA82C519E76E8F51A88FD599B989B007C58A8CC550023C63B4AD9F4BE4D1443663DF0C55297396F6D04998553C0CCF3D2C951E119F694726F2D5ACA14FD21A6ADABBD3A785BC7E7F4EFA7CB7C14CF3E9CC2EF2F0688A621EF65549198BBEF11A4472CB9C47D5FF03CE2C7E7ED7B03E81143366FDF1338BFB16D42CEBE57A0BD31552D014C26B6E0E905DF23659E5DA769170497FA15F8B6ECDD6E5D26D58E1A77793E3F3E3A3A9168EC2091FC2F16102DE2E1FC4102427C95651255BBD3A2CCA344BEA8609927699C6CA3B580B9D0CED282AE78D942146B2ED116A5D5CAC59366D593E602DAB3450B58585A4D0CE072EDF503DF38324DC3DE04A4B363454AF8A1DAA7F1563D5337E568EB6E830836D6F05BA47E07DB414D1CDA70BBE8917D18EFE66CF3339452E275C8D9B06B09102D3FECA1D7BFF9B677C34F43BF14B73730AA9936E094735B682F0334F69785D3968D32F6306DE30C3F18D9ACE84AFBCC54D0D1573F76D86FE0F65307B88C4D602DB0D4BCC01452129A2C02383CDFBB247071C9002C5271E8B2A00EBFDE5761606D024DF0F8C09134ECFD26D713F603368DB2503F2A1A466390C02438189B910D52CF89052D7350162400960743CAC61103E5A36FFE5584F22D2C60F4354F34851C78A21FC02B7A7AECE80DDA800FC2048460548DE0E2ACF122090EDA40F3A2D004E2005FB932866F705A8908E829741789C91D86BC4CC0B90BBD54FBFE0A848B0A0F2E10BA87C2020944B3687D4AB751B2EA4E2D34BB4D3F6643688F7240A3C16151DA039B811D7FA377D9D3E087F730071C7E971568F2F1A737537BDB2AD86C1ECDBEEAC31B757B3FF6DE8CB9FA5952F701DB6B9B30D81983BB0130F12903775DB1B72DA379DD871600A1E6C0F78B9A8BA0151D4EBD5FE465C1E37E717FC5217858895387536F16D95CB6CFC0EB9EAC34B0696F9C4870150E16A294D30500ED2AC7910EFDABBEFE0D47F343B5B6096781244448C1FA0CBD7EC80CAA98B1C58EA954D74F569ABC0B95B090DA51A4C5FC82EDA802A37A2ED2219B710A89815E3A0C2B31241F432131A4F67B9418D5D3907B2E31C05BE0610566AAE5686271B15E91024ACB55CA3D39450D59F17928495AC823636DB80FFB7AD2ECAA8DBA6E83A9B8FAE6B1A9F3F9EA4B86C5A089DEE69A48EF3C023DD2F02CA0335A05F753D726C8A20FDE7E963BE2EBC1DED826B63D2ABBD2F56106AE26444B8235F2C4AB214127E510ECBACA1232F56FC0F069ADB297F6953C435F6DB08FD44F5B03F5412AADE193B0115527A45AD353DDC2A23BEA63947BA23560274DA50D7C729805C0273530FCBAD21ABE72F0857A4D5FB602C0EFB7E4B9C25543FDBD2AB6EF51D92CB2B69D750B9FB2C3AE89BA53DAC6DCB1B4924BFD4A2DF4DDBAD14AB6115A62491B7DB764BFE4D2373148B57D9336FABE89E56DEA9BCD2F957BE56AA1FEB837ACC5BEF8372515C94C33A615AFF1E1C417D84BD592C59449068AECEA643EA34562A62C4F8405817CD20E409E26AB0772CD333892924909EB565C1565F029E360D21CC6BA2F715C3A868A3E75CEC660120153A2FB92960F26554A3D0028D5A727F0222718BB8DD0B5851A72056BA67916989679239337B0D4A4AA63F1FB223DDDD8F2E1E46AA23561E75E8886EC49F66352E175F6F2A0F5935817713C948C29465F080D86065E173DCC13CD1BE7E4D16E52A61B73DEE8661FFBF647221F04ABA653132CDB6FDD844805C655A8F14C76BBEC99E8569C4779B0A0A622BD952413E98AB8C07E823A19E9AAE83D907CAB503F2F737C4C335311B266A4D8C2ECEC4B6E00D353A7B335415C5EC8B334550790C6FB38D414AA439606DBD6E1662D187403106D0ECEF1B044816E62E6637F620CC79718E91E6F890A473A104901B9350CF116E08914EB776B28E72A744A4BE1BDE3A074958399A008160018611356A03FF5632891EA2C59C239F9249E905AEF4CA15E393353A093F3D199C2791F25A6905AEF4C214E61334F80B3E1D159E273EAD02B9FDA13CDB6EE6CD178574901FEB3CCF2E81EBDCB56685DD4A5678B9B5D5ADDFFDAFC75898AE4BE03718661A6CDF9640794B6B94EEF327A902B60449B08F75BBDC38BE12A2AA3577999DC45D50E37ABAE7EADFDC0F5759AD505C45F1036BA3EECCAEDAEC424A3CD9735E7EFA90E8475FD9F2D249CCF3ED4EFB2153E48C06826D595B91FD2D7BB64BD6AF17E03DCC6A800519D34938B56ABB12CAB0B57EFBFB590DED72FE3D80022EC6B0FC83FA2CD768D81151FD2DBE82BEA831B16BFB7E83E8ABF75776FAA8098078267FBD96512DDE7D1A62030BAEFF19F5886579BC73FFF3FEDD68FE93D730100 , N'6.2.0-61023')

