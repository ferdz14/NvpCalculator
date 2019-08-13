USE [NpvDB]
GO

/****** Object:  Table [dbo].[Calculation]    Script Date: 8/9/2019 3:52:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Calculation](
	[CalculationID] [int] IDENTITY(1,1) NOT NULL,
	[LowerBoundDiscountRate] [float] NOT NULL,
	[UpperBoundDiscountRate] [float] NOT NULL,
	[DiscountRateIncrement] [float] NOT NULL,
	[InitialInvestment] [float] NOT NULL,
 CONSTRAINT [PK_Calculation] PRIMARY KEY CLUSTERED 
(
	[CalculationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [NpvDB]
GO

/****** Object:  Table [dbo].[CashFlow]    Script Date: 8/9/2019 3:52:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CashFlow](
	[CashFlowID] [int] IDENTITY(1,1) NOT NULL,
	[CashFlowValue] [float] NOT NULL,
	[CalculationID] [int] NOT NULL,
 CONSTRAINT [PK_CashFlow] PRIMARY KEY CLUSTERED 
(
	[CashFlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO