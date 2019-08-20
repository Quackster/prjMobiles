-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Aug 20, 2019 at 11:35 AM
-- Server version: 10.3.13-MariaDB
-- PHP Version: 7.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `prjmobiles`
--

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `id` int(11) NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `model_type` tinyint(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`id`, `name`, `model_type`) VALUES
(1, 'Main room', 0),
(2, 'Disco room', 1);

-- --------------------------------------------------------

--
-- Table structure for table `rooms_models`
--

CREATE TABLE `rooms_models` (
  `model_id` int(11) NOT NULL,
  `start_x` int(11) NOT NULL,
  `start_y` int(11) NOT NULL,
  `start_z` int(11) NOT NULL,
  `start_rotation` int(11) NOT NULL,
  `heightmap` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `objects` text COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `rooms_models`
--

INSERT INTO `rooms_models` (`model_id`, `start_x`, `start_y`, `start_z`, `start_rotation`, `heightmap`, `objects`) VALUES
(0, 2, 0, 0, 4, 'xxx0xxxxxxxxxxxxx|0000000xxx00000xx|0000000x0000000xx|0000000xxxxxxxxxx|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|00000000000000000|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x|0000000000000000x', '1 bardesk 14 3 0 6\r\n2 bardesk 13 3 0 6\r\n3 bardesk 12 3 0 6\r\n4 bardesk 11 3 0 6\r\n5 bardesk 10 3 0 6\r\n6 bardesk 9 3 0 6\r\n7 bardesk 8 3 0 6\r\n8 bardesk 7 3 0 0\r\n9 bardesk 7 2 0 0\r\n10 bardesk 7 1 0 0\r\n11 bardesk 15 3 0 6\r\n12 bardesk 15 2 0 0\r\n13 bardesk 15 1 0 4\r\n14 fridgeB 8 1 0 4\r\n15 fridgeA 9 1 0 4\r\n16 chair 0 11 0 2\r\n17 chair 1 13 0 0\r\n18 chair 3 11 0 6\r\n19 chair 1 10 0 4\r\n20 table 1 11 0 2 2\r\n21 chair 0 17 0 2\r\n22 chair 1 19 0 0\r\n23 chair 3 17 0 6\r\n24 chair 0 15 0 4\r\n25 table 1 17 0 2 2\r\n26 chair 0 22 0 2\r\n27 chair 1 24 0 0\r\n28 chair 3 22 0 6\r\n29 chair 1 21 0 4\r\n30 table 1 22 0 2 2\r\n31 chair 9 12 0 2\r\n33 chair 12 12 0 6\r\n34 chair 10 10 0 4\r\n35 table 10 11 0 2 2\r\n36 chair 11 22 0 2\r\n37 chair 12 24 0 0\r\n38 chair 14 22 0 6\r\n39 chair 12 21 0 4\r\n40 table 12 22 0 2 2\r\n41 chair 5 22 0 2\r\n42 table 6 21 0 2 2\r\n43 chair 7 24 0 4\r\n43 chair 8 22 0 6'),
(1, 2, 0, 0, 4, 'X6666666665432100|X6666666665432100|X6600000000000X00|X6600000000000000|X6600000000000000|X6600000000000000|X660000000000X000|666000000000X1111|X66000000000XX111|X66000000000X1111|X66000000000X1111|X55000000000X1111|X44000000000X1111|X33000000000X1111|X22000000000XX111|X11X00000000X1111|X00000000000X1111|X00000000000XX111', '');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `username` varchar(16) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `age` int(3) NOT NULL,
  `pants` int(2) NOT NULL,
  `shirt` int(2) NOT NULL,
  `head` int(2) NOT NULL,
  `sex` char(1) COLLATE utf8mb4_unicode_ci NOT NULL,
  `mission` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` bigint(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`username`, `password`, `email`, `age`, `pants`, `shirt`, `head`, `sex`, `mission`, `created_at`) VALUES
('Alex', '123', 'you@domain.com', 15, 9, 9, 9, 'M', 'Alex the best', 0),
('DanTheMan', '123', 'you@domain.com', 15, 3, 5, 6, 'M', 'Alex the best', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `rooms_models`
--
ALTER TABLE `rooms_models`
  ADD PRIMARY KEY (`model_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`username`),
  ADD UNIQUE KEY `username` (`username`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
