/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Email]
      ,[Username]
      ,[Password]
  FROM [mtg_deckbuilder].[dbo].[Accounts]