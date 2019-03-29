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
