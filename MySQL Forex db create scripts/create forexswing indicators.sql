use forexswing;

CREATE TABLE `ccidata` (
  `PairName` char(10) NOT NULL,
  `period` int(11) NOT NULL,
  `BarTime` datetime NOT NULL,
  `EntryCCI` double NOT NULL,
  `TrendCCI` double NOT NULL,
  `GoldBar` tinyint(1) DEFAULT NULL,
  `TrendCCIcolor` varchar(16) DEFAULT NULL,
  `EntryCCIdirection` varchar(16) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `heikenashidata` (
  `PairName` char(10) NOT NULL,
  `period` int(11) NOT NULL,
  `BarTime` datetime NOT NULL,
  `open` double NOT NULL,
  `high` double NOT NULL,
  `low` double NOT NULL,
  `close` double NOT NULL,
  `color` char(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `ichimokudata` (
  `PairName` char(10) NOT NULL,
  `BarPeriod` int(11) NOT NULL,
  `BarTime` datetime NOT NULL,
  `Tenkan_sen` double NOT NULL,
  `Kijun_sen` double NOT NULL,
  `SpanA` double NOT NULL,
  `SpanB` double NOT NULL,
  `TenkanPeriod` int(11) DEFAULT NULL,
  `KijunPeriod` int(11) DEFAULT NULL,
  `SpanBPeriod` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `rvidata` (
  `pairname` varchar(10) NOT NULL,
  `period` int(11) NOT NULL,
  `bartime` datetime NOT NULL,
  `RVIvalue` double NOT NULL,
  `RVIsigvalue` double NOT NULL,
  `RVIdirection` varchar(16) DEFAULT NULL,
  `RVIsigdirection` varchar(16) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `trixdata` (
  `PairName` char(6) NOT NULL,
  `Period` int(11) NOT NULL,
  `BarTime` datetime NOT NULL,
  `FastTrix` double NOT NULL,
  `SlowTrix` double NOT NULL,
  `FastDirection` varchar(16) NOT NULL,
  `SlowDirection` varchar(16) NOT NULL,
  `FastColor` varchar(8) NOT NULL,
  `SlowColor` varchar(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `stochastics` (
  `PairName` varchar(8) NOT NULL,
  `BarDateTime` datetime NOT NULL,
  `BarPeriod` int(11) NOT NULL,
  `LookbackPeriod` int(11) NOT NULL,
  `K_slowmult` int(11) NOT NULL,
  `D_mult` int(11) NOT NULL,
  `PercentK` float NOT NULL,
  `PercentKslow` float NOT NULL,
  `PercentD` float NOT NULL,
  `PercentDslow` float NOT NULL,
  PRIMARY KEY (`PairName`,`BarDateTime`,`BarPeriod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


