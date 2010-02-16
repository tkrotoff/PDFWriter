﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    static class FontMetrics
    {
        /// <summary>
        /// Courier.
        /// </summary>
        private static double[] FC = new double[] {
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
            .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600, .600,
        };

        /// <summary>
        /// Helvetica.
        /// </summary>
        private static double[] FH = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333,
            .278, .278, .355, .556, .556, .889, .667, .191, .333, .333, .389, .584, .278, .333, .278, .278,
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .278, .278, .584, .584, .584, .556,
            1.015, .667, .667, .722, .722, .667, .611, .778, .722, .278, .500, .667, .556, .833, .722, .778,
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .278, .278, .278, .469, .556,
            .333, .556, .556, .500, .556, .556, .278, .556, .556, .222, .222, .500, .222, .833, .556, .556,
            .556, .556, .333, .500, .278, .556, .500, .722, .500, .500, .500, .334, .260, .334, .584, .278,
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .333, .333, .333, .222,
            .222, .222, 1.00, .500, .500, .556, 1.00, .667, .667, .611, .278, .222, .944, .500, .500, .278,
            .278, .333, .556, .556, .556, .556, .260, .556, .333, .737, .370, .556, .584, .278, .737, .333,
            .400, .584, .333, .333, .333, .556, .537, .278, .333, .333, .365, .556, .834, .834, .834, .611,
            .667, .667, .667, .667, .667, .667, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278,
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611,
            .556, .556, .556, .556, .556, .556, .889, .500, .556, .556, .556, .556, .278, .278, .278, .278,
            .556, .556, .556, .556, .556, .556, .556, .584, .611, .556, .556, .556, .556, .500, .556, .500,
        };

        /// <summary>
        /// Helvetica-Bold.
        /// </summary>
        private static double[] FHB = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333,
            .278, .333, .474, .556, .556, .889, .722, .238, .333, .333, .389, .584, .278, .333, .278, .278,
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .333, .333, .584, .584, .584, .611,
            .975, .722, .722, .722, .722, .667, .611, .778, .722, .278, .556, .722, .611, .833, .722, .778,
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .333, .278, .333, .584, .556,
            .333, .556, .611, .556, .611, .556, .333, .611, .611, .278, .278, .556, .278, .889, .611, .611,
            .611, .611, .389, .556, .333, .611, .556, .778, .556, .556, .500, .389, .280, .389, .584, .278,
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .500, .500, .500, .278,
            .278, .278, 1.00, .611, .611, .611, 1.00, .667, .667, .611, .278, .278, .944, .556, .500, .278,
            .278, .333, .556, .556, .556, .556, .280, .556, .333, .737, .370, .556, .584, .278, .737, .333,
            .400, .584, .333, .333, .333, .611, .556, .278, .333, .333, .365, .556, .834, .834, .834, .611,
            .722, .722, .722, .722, .722, .722, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278,
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611,
            .556, .556, .556, .556, .556, .556, .889, .556, .556, .556, .556, .556, .278, .278, .278, .278,
            .611, .611, .611, .611, .611, .611, .611, .584, .611, .611, .611, .611, .611, .556, .611, .556,
        };

        /// <summary>
        /// Helvetica-Oblique.
        /// </summary>
        private static double[] FHI = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333,
            .278, .278, .355, .556, .556, .889, .667, .191, .333, .333, .389, .584, .278, .333, .278, .278,
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .278, .278, .584, .584, .584, .556,
            1.015, .667, .667, .722, .722, .667, .611, .778, .722, .278, .500, .667, .556, .833, .722, .778,
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .278, .278, .278, .469, .556,
            .333, .556, .556, .500, .556, .556, .278, .556, .556, .222, .222, .500, .222, .833, .556, .556,
            .556, .556, .333, .500, .278, .556, .500, .722, .500, .500, .500, .334, .260, .334, .584, .278,
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .333, .333, .333, .222,
            .222, .222, 1.00, .500, .500, .556, 1.00, .667, .667, .611, .278, .222, .944, .500, .500, .278,
            .278, .333, .556, .556, .556, .556, .260, .556, .333, .737, .370, .556, .584, .278, .737, .333,
            .400, .584, .333, .333, .333, .556, .537, .278, .333, .333, .365, .556, .834, .834, .834, .611,
            .667, .667, .667, .667, .667, .667, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278,
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611,
            .556, .556, .556, .556, .556, .556, .889, .500, .556, .556, .556, .556, .278, .278, .278, .278,
            .556, .556, .556, .556, .556, .556, .556, .584, .611, .556, .556, .556, .556, .500, .556, .500,
        };

        /// <summary>
        /// Helvetica-BoldOblique.
        /// </summary>
        private static double[] FHBI = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333,
            .278, .333, .474, .556, .556, .889, .722, .238, .333, .333, .389, .584, .278, .333, .278, .278,
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .333, .333, .584, .584, .584, .611,
            .975, .722, .722, .722, .722, .667, .611, .778, .722, .278, .556, .722, .611, .833, .722, .778,
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .333, .278, .333, .584, .556,
            .333, .556, .611, .556, .611, .556, .333, .611, .611, .278, .278, .556, .278, .889, .611, .611,
            .611, .611, .389, .556, .333, .611, .556, .778, .556, .556, .500, .389, .280, .389, .584, .278,
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .500, .500, .500, .278,
            .278, .278, 1.00, .611, .611, .611, 1.00, .667, .667, .611, .278, .278, .944, .556, .500, .278,
            .278, .333, .556, .556, .556, .556, .280, .556, .333, .737, .370, .556, .584, .278, .737, .333,
            .400, .584, .333, .333, .333, .611, .556, .278, .333, .333, .365, .556, .834, .834, .834, .611,
            .722, .722, .722, .722, .722, .722, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278,
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611,
            .556, .556, .556, .556, .556, .556, .889, .556, .556, .556, .556, .556, .278, .278, .278, .278,
            .611, .611, .611, .611, .611, .611, .611, .584, .611, .611, .611, .611, .611, .556, .611, .556,
        };

        /// <summary>
        /// Times.
        /// </summary>
        private static double[] FT = new double[] {
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .333, .333, .333, .333, .333, .333, .333, .333,
            .250, .333, .408, .500, .500, .833, .778, .180, .333, .333, .500, .564, .250, .333, .250, .278,
            .500, .500, .500, .500, .500, .500, .500, .500, .500, .500, .278, .278, .564, .564, .564, .444,
            .921, .722, .667, .667, .722, .611, .556, .722, .722, .333, .389, .722, .611, .889, .722, .722,
            .556, .722, .667, .556, .611, .722, .722, .944, .722, .722, .611, .333, .278, .333, .469, .500,
            .333, .444, .500, .444, .500, .444, .333, .500, .500, .278, .278, .500, .278, .778, .500, .500,
            .500, .500, .333, .389, .278, .500, .500, .722, .500, .500, .444, .480, .200, .480, .541, .250,
            .350, .500, .500, 1.00, 1.00, .500, .500, .167, .333, .333, .564, 1.00, .444, .444, .444, .333,
            .333, .333, .980, .556, .556, .611, .889, .556, .722, .611, .278, .278, .722, .389, .444, .250,
            .250, .333, .500, .500, .500, .500, .200, .500, .333, .760, .276, .500, .564, .250, .760, .333,
            .400, .564, .300, .300, .333, .500, .453, .250, .333, .300, .310, .500, .750, .750, .750, .444,
            .722, .722, .722, .722, .722, .722, .889, .667, .611, .611, .611, .611, .333, .333, .333, .333,
            .722, .722, .722, .722, .722, .722, .722, .564, .722, .722, .722, .722, .722, .722, .556, .500,
            .444, .444, .444, .444, .444, .444, .667, .444, .444, .444, .444, .444, .278, .278, .278, .278,
            .500, .500, .500, .500, .500, .500, .500, .564, .500, .500, .500, .500, .500, .500, .500, .500,
        };

        /// <summary>
        /// Times-Bold.
        /// </summary>
        private static double[] FTB = new double[] {
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .333, .333, .333, .333, .333, .333, .333, .333,
            .250, .333, .555, .500, .500, 1.00, .833, .278, .333, .333, .500, .570, .250, .333, .250, .278,
            .500, .500, .500, .500, .500, .500, .500, .500, .500, .500, .333, .333, .570, .570, .570, .500,
            .930, .722, .667, .722, .722, .667, .611, .778, .778, .389, .500, .778, .667, .944, .722, .778,
            .611, .778, .722, .556, .667, .722, .722, 1.00, .722, .722, .667, .333, .278, .333, .581, .500,
            .333, .500, .556, .444, .556, .444, .333, .500, .556, .278, .333, .556, .278, .833, .556, .500,
            .556, .556, .444, .389, .333, .556, .500, .722, .500, .500, .444, .394, .220, .394, .520, .250,
            .350, .500, .500, 1.00, 1.00, .500, .500, .167, .333, .333, .570, 1.00, .500, .500, .500, .333,
            .333, .333, 1.00, .556, .556, .667, 1.00, .556, .722, .667, .278, .278, .722, .389, .444, .250,
            .250, .333, .500, .500, .500, .500, .220, .500, .333, .747, .300, .500, .570, .250, .747, .333,
            .400, .570, .300, .300, .333, .556, .540, .250, .333, .300, .330, .500, .750, .750, .750, .500,
            .722, .722, .722, .722, .722, .722, 1.00, .722, .667, .667, .667, .667, .389, .389, .389, .389,
            .722, .722, .778, .778, .778, .778, .778, .570, .778, .722, .722, .722, .722, .722, .611, .556,
            .500, .500, .500, .500, .500, .500, .722, .444, .444, .444, .444, .444, .278, .278, .278, .278,
            .500, .556, .500, .500, .500, .500, .500, .570, .500, .556, .556, .556, .556, .500, .556, .500,
        };

        /// <summary>
        /// Times-Italic.
        /// </summary>
        private static double[] FTI = new double[] {
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .333, .333, .333, .333, .333, .333, .333, .333,
            .250, .333, .420, .500, .500, .833, .778, .214, .333, .333, .500, .675, .250, .333, .250, .278,
            .500, .500, .500, .500, .500, .500, .500, .500, .500, .500, .333, .333, .675, .675, .675, .500,
            .920, .611, .611, .667, .722, .611, .611, .722, .722, .333, .444, .667, .556, .833, .667, .722,
            .611, .722, .611, .500, .556, .722, .611, .833, .611, .556, .556, .389, .278, .389, .422, .500,
            .333, .500, .500, .444, .500, .444, .278, .500, .500, .278, .278, .444, .278, .722, .500, .500,
            .500, .500, .389, .389, .278, .500, .444, .667, .444, .444, .389, .400, .275, .400, .541, .250,
            .350, .500, .500, .889, .889, .500, .500, .167, .333, .333, .675, 1.00, .556, .556, .556, .333,
            .333, .333, .980, .500, .500, .556, .944, .500, .556, .556, .278, .278, .667, .389, .389, .250,
            .250, .389, .500, .500, .500, .500, .275, .500, .333, .760, .276, .500, .675, .250, .760, .333,
            .400, .675, .300, .300, .333, .500, .523, .250, .333, .300, .310, .500, .750, .750, .750, .500,
            .611, .611, .611, .611, .611, .611, .889, .667, .611, .611, .611, .611, .333, .333, .333, .333,
            .722, .667, .722, .722, .722, .722, .722, .675, .722, .722, .722, .722, .722, .556, .611, .500,
            .500, .500, .500, .500, .500, .500, .667, .444, .444, .444, .444, .444, .278, .278, .278, .278,
            .500, .500, .500, .500, .500, .500, .500, .675, .500, .500, .500, .500, .500, .444, .500, .444,
        };

        /// <summary>
        /// Times-BoldItalic.
        /// </summary>
        private static double[] FTBI = new double[] {
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .333, .333, .333, .333, .333, .333, .333, .333,
            .250, .389, .555, .500, .500, .833, .778, .278, .333, .333, .500, .570, .250, .333, .250, .278,
            .500, .500, .500, .500, .500, .500, .500, .500, .500, .500, .333, .333, .570, .570, .570, .500,
            .832, .667, .667, .667, .722, .667, .667, .722, .778, .389, .500, .667, .611, .889, .722, .722,
            .611, .722, .667, .556, .611, .722, .667, .889, .667, .611, .611, .333, .278, .333, .570, .500,
            .333, .500, .500, .444, .500, .444, .333, .500, .556, .278, .278, .500, .278, .778, .556, .500,
            .500, .500, .389, .389, .278, .556, .444, .667, .500, .444, .389, .348, .220, .348, .570, .250,
            .350, .500, .500, 1.00, 1.00, .500, .500, .167, .333, .333, .606, 1.00, .500, .500, .500, .333,
            .333, .333, 1.00, .556, .556, .611, .944, .556, .611, .611, .278, .278, .722, .389, .389, .250,
            .250, .389, .500, .500, .500, .500, .220, .500, .333, .747, .266, .500, .606, .250, .747, .333,
            .400, .570, .300, .300, .333, .576, .500, .250, .333, .300, .300, .500, .750, .750, .750, .500,
            .667, .667, .667, .667, .667, .667, .944, .667, .667, .667, .667, .667, .389, .389, .389, .389,
            .722, .722, .722, .722, .722, .722, .722, .570, .722, .722, .722, .722, .722, .611, .611, .500,
            .500, .500, .500, .500, .500, .500, .722, .444, .444, .444, .444, .444, .278, .278, .278, .278,
            .500, .556, .500, .500, .500, .500, .500, .570, .500, .556, .556, .556, .556, .444, .500, .444,
        };

        /// <summary>
        /// Symbol.
        /// </summary>
        private static double[] FS = new double[] {
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .333, .713, .500, .549, .833, .778, .439, .333, .333, .500, .549, .250, .549, .250, .278,
            .500, .500, .500, .500, .500, .500, .500, .500, .500, .500, .278, .278, .549, .549, .549, .444,
            .549, .722, .667, .722, .612, .611, .763, .603, .722, .333, .631, .722, .686, .889, .722, .722,
            .768, .741, .556, .592, .611, .690, .439, .768, .645, .795, .611, .333, .863, .333, .658, .500,
            .500, .631, .549, .549, .494, .439, .521, .411, .603, .329, .603, .549, .549, .576, .521, .549,
            .549, .521, .549, .603, .439, .576, .713, .686, .493, .686, .494, .480, .200, .480, .549, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250, .250,
            .250, .620, .247, .549, .167, .713, .500, .753, .753, .753, .753, 1.042, .987, .603, .987, .603,
            .400, .549, .411, .549, .549, .713, .494, .460, .549, .549, .549, .549, 1.00, .603, 1.00, .658,
            .823, .686, .795, .987, .768, .768, .823, .768, .768, .713, .713, .713, .713, .713, .713, .713,
            .768, .713, .790, .790, .890, .823, .549, .250, .713, .603, .603, 1.042, .987, .603, .987, .603,
            .494, .329, .790, .790, .786, .713, .384, .384, .384, .384, .384, .384, .494, .494, .494, .494,
            .250, .329, .274, .686, .686, .686, .384, .384, .384, .384, .384, .384, .494, .494, .494, .250,
        };

        /// <summary>
        /// ZapfDingbats.
        /// </summary>
        private static double[] FZ = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .974, .961, .974, .980, .719, .789, .790, .791, .690, .960, .939, .549, .855, .911, .933,
            .911, .945, .974, .755, .846, .762, .761, .571, .677, .763, .760, .759, .754, .494, .552, .537,
            .577, .692, .786, .788, .788, .790, .793, .794, .816, .823, .789, .841, .823, .833, .816, .831,
            .923, .744, .723, .749, .790, .792, .695, .776, .768, .792, .759, .707, .708, .682, .701, .826,
            .815, .789, .789, .707, .687, .696, .689, .786, .787, .713, .791, .785, .791, .873, .761, .762,
            .762, .759, .759, .892, .892, .788, .784, .438, .138, .277, .415, .392, .392, .668, .668, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278,
            .278, .732, .544, .544, .910, .667, .760, .760, .776, .595, .694, .626, .788, .788, .788, .788,
            .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788,
            .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788, .788,
            .788, .788, .788, .788, .894, .838, 1.016, .458, .748, .924, .748, .918, .927, .928, .928, .834,
            .873, .828, .924, .924, .917, .930, .931, .463, .883, .836, .836, .867, .867, .696, .696, .874,
            .278, .874, .760, .946, .771, .865, .771, .888, .967, .888, .831, .873, .927, .970, .918, .278,
        };

        public static double GetTextWidth(string text, Font font)
        {
            double width = 0;

            foreach (char ch in text)
            {
                double ws = 0;
                if (ch == 32)
                {
                    //Width of a space character
                    ws = font.WordSpace;
                }
                double charWidth = 0;
                if (font.Name == Font.Helvetica)
                {
                    charWidth = FH[ch];
                }
                else if (font.Name == Font.HelveticaBold)
                {
                    charWidth = FHB[ch];
                }
                width += charWidth + font.CharSpace + ws;
            }

            return width * font.Size;
        }
    }
}
