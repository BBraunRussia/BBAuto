create procedure [dbo].[InsertMailTexts]
as
  insert into dbo.MailText values(N'Водительское удостоверение', N'Добрый день, UserName!    Напоминаем, что у вас DateEnd года заканчивается водительское удостоверение.  Просим своевременно оформить новое.  Скан копию необходимо прислать в отдел кадров и транспортный отдел.    С уважением,  Транспортный отдел')
  insert into dbo.MailText values(N'Медицинская справка', N'Добрый день, UserName!    Напоминаем, что у вас DateEnd года заканчивается медицинская справка.  Просим своевременно оформить новую.  Оригинал необходимо прислать в отдел кадров, а скан копию в транспортный отдел.    С уважением,  Транспортный отдел')
  insert into dbo.MailText values(N'Страховые полисы', N'Добрый день!    В следующем месяце заканчиваются следующие страховые полюса.    List    С уважением,  Система рассылки уведомлений')
  insert into dbo.MailText values(N'Диагностические карты', N'Добрый день!    В следующем месяце заканчиваются следующие диагностические карты.    List    С уважением,  Система рассылки уведомлений')