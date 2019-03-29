CREATE DATABASE `forexswing` /*!40100 DEFAULT CHARACTER SET utf8 */;

CREATE TABLE `audusd` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `audusd1440` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `audusd60` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `eurusd` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `eurusd1440` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `eurusd60` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `gbpusd` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `gbpusd1440` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `gbpusd60` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `nzdusd1440` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `nzdusd60` (
  `BarTime` datetime NOT NULL,
  `Open` double NOT NULL,
  `High` double NOT NULL,
  `Low` double NOT NULL,
  `Close` double NOT NULL,
  `Volume` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;








