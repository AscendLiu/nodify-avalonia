﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a line that has an arrow indicating its <see cref="BaseConnection.Direction"/>.
    /// </summary>
    public class LineConnection : BaseConnection
    {
        static LineConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineConnection), new FrameworkPropertyMetadata(typeof(LineConnection)));
        }

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1) = GetLinePoints(source, target);

            using var _ = context.BeginFigure(source, false, false);
            context.LineTo(p0, true, true);
            context.LineTo(p1, true, true);
            context.LineTo(target, true, true);

            return ((target, source), (source, target));
        }

        protected override void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, Orientation orientation = Orientation.Horizontal)
        {
            if (Spacing < 1d)
            {
                Vector delta = source - target;
                double headWidth = ArrowSize.Width;
                double headHeight = ArrowSize.Height / 2;

                double angle = Math.Atan2(delta.Y, delta.X);
                double sinT = Math.Sin(angle);
                double cosT = Math.Cos(angle);

                var from = new Point(target.X + (headWidth * cosT - headHeight * sinT), target.Y + (headWidth * sinT + headHeight * cosT));
                var to = new Point(target.X + (headWidth * cosT + headHeight * sinT), target.Y - (headHeight * cosT - headWidth * sinT));

                using var _ = context.BeginFigure(target, true, true);
                context.LineTo(from, true, true);
                context.LineTo(to, true, true);
            }
            else
            {
                base.DrawDefaultArrowhead(context, source, target, arrowDirection, orientation);
            }
        }

        protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1) = GetLinePoints(source, target);
            var direction = p0 - p1;

            double spacing = 1d / (DirectionalArrowsCount + 1);
            for (int i = 1; i <= DirectionalArrowsCount; i++)
            {
                double t = spacing * i;
                var to = InterpolateLineSegment(p0, p1, t);

                base.DrawDirectionalArrowheadGeometry(context, direction, to);
            }
        }

        private (Point P0, Point P1) GetLinePoints(Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var spacingVertical = new Vector(spacing.Y, spacing.X);

            var p0 = source + (SourceOrientation == Orientation.Vertical ? spacingVertical : spacing);
            var p1 = target - (TargetOrientation == Orientation.Vertical ? spacingVertical : spacing);

            return (p0, p1);
        }

        protected static Point InterpolateLineSegment(Point p0, Point p1, double t)
        {
            return (Point)((1 - t) * (Vector)p0 + t * (Vector)p1);
        }
    }
}
