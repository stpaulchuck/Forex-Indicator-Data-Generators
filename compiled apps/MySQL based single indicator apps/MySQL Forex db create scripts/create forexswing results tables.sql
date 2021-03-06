CREATE TABLE `profitevents` (
  `BarTime` datetime NOT NULL,
  `Direction` varchar(16) NOT NULL DEFAULT 'none',
  `TotalProfit` double NOT NULL DEFAULT '0',
  `PairName` varchar(6) NOT NULL,
  `BarPeriod` int(11) NOT NULL DEFAULT '1440',
  `TurnBarTime` datetime NOT NULL,
  PRIMARY KEY (`BarTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='generated by correlation analyzer supplemental data generator';

CREATE TABLE `testresults2` (
  `PairName` char(6) NOT NULL,
  `TriggerDate` datetime NOT NULL,
  `CloseDate` datetime NOT NULL,
  `LongOrShort` varchar(50) NOT NULL,
  `OpenPrice` double NOT NULL,
  `ClosePrice` double NOT NULL,
  `NetProfitOrLoss` double NOT NULL,
  `TestType` int(11) NOT NULL,
  `MaxRetraceAmt` double NOT NULL,
  `ProfitableBarcount` int(11) NOT NULL,
  `BarsFromCCICrossover` int(11) NOT NULL,
  `AfterGoldBar` tinyint(1) NOT NULL,
  `FallingBarCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `testresultscorranlyzr` (
  `PairName` char(6) NOT NULL,
  `BarPeriod` int(11) NOT NULL DEFAULT '1440',
  `ProfitStartDate` datetime NOT NULL,
  `LongOrShort` varchar(50) NOT NULL,
  `CompareItemName` varchar(45) NOT NULL,
  `CompareItemDate` datetime NOT NULL,
  `TestType` varchar(45) NOT NULL,
  `Success` bit(1) NOT NULL DEFAULT b'0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `testresultsforreversals` (
  `PairName` char(6) NOT NULL,
  `TriggerDate` datetime NOT NULL,
  `TestNumber` int(11) NOT NULL,
  `CloseDate` datetime NOT NULL,
  `LongOrShort` varchar(50) NOT NULL,
  `OpenPrice` double NOT NULL,
  `ClosePrice` double NOT NULL,
  `NetProfitOrLoss` double NOT NULL,
  `MaxRetraceAmt` double NOT NULL,
  `ProfitableBarcount` int(11) NOT NULL,
  `IsCCIdip` tinyint(1) NOT NULL,
  `CCIdipNumber` int(11) NOT NULL,
  `ExitOnCCIdivergence` tinyint(1) NOT NULL,
  `ExitOnCCIspike` tinyint(1) NOT NULL,
  `ExitOnFixedStopLoss` tinyint(1) NOT NULL,
  `ExitOnFixedTakeProfit` tinyint(1) NOT NULL,
  `ExitOnReversalBar` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `testresultshookandretrace` (
  `PairName` char(6) NOT NULL,
  `BarPeriod` int(11) NOT NULL,
  `TriggerDate` datetime NOT NULL,
  `TrixCrossingDate` datetime NOT NULL,
  `RVI3CrossingDate` datetime NOT NULL,
  `CloseDate` datetime NOT NULL,
  `LongOrShort` varchar(50) NOT NULL,
  `OpenPrice` double NOT NULL,
  `ClosePrice` double NOT NULL,
  `NetProfitOrLoss` double NOT NULL,
  `TestType` int(11) NOT NULL,
  `MaxRetraceAmt` double NOT NULL,
  `ProfitableBarcount` int(11) NOT NULL,
  `ExitOnCCISpike` tinyint(1) NOT NULL,
  `ExitOnFixedStopLoss` tinyint(1) NOT NULL,
  `ExitOnFixedTakeProfit` tinyint(1) NOT NULL,
  `ExitOnReversalBar` tinyint(1) NOT NULL,
  `ExitOnCCIdivergence` tinyint(1) NOT NULL,
  `ReentryNumber` int(11) NOT NULL,
  `IsEarlyEntry` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `testresultslondon2ny` (
  `PairName` varchar(50) NOT NULL,
  `LondonStartDate` datetime NOT NULL,
  `LondonStopDate` datetime DEFAULT NULL,
  `TriggerDate` datetime NOT NULL,
  `CloseDate` datetime NOT NULL,
  `LongOrShort` varchar(50) NOT NULL,
  `OpenPrice` double NOT NULL,
  `ClosePrice` double NOT NULL,
  `NetProfitOrLoss` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
