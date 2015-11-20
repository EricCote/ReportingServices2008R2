//-----------------------------------------------------------------------
//  This file is part of the Microsoft Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
// 
//  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//-----------------------------------------------------------------------

namespace Microsoft.Samples.ReportingServices
{
    #region Using directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.ReportDesigner;
    using Microsoft.ReportDesigner.Design;
    using Microsoft.ReportingServices.Interfaces;
    using Rdl = Microsoft.ReportingServices.RdlObjectModel;
    using System.Xml;
    using System.Xml.Xsl;
    using System.Xml.XPath;

    #endregion

    // implementation of the adornment class, which allows us to 
    // draw outside of the main rectangle of the design surface
    // and handle UI events
    internal sealed class PolygonsDesignWindow : Adornment
    {
        private const int BorderWidth = 4;

        private AdornerService adornerSvc;
        private Font font;
        private Rectangle bounds;
        private int frameHeight;
        private int frameWidth;
        private PolygonsDesigner component;
        private Frame shapeFrame;
        private Frame pointFrame;
        private Frame coordinateFrameX;
        private Frame coordinateFrameY;
        private Frame[] frames;
        private StringFormat textFormat;

        // private IDesignerHost host;

        public PolygonsDesignWindow(CustomReportItemDesigner component)
        {
            this.component = (PolygonsDesigner)component;

            // this.host = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
            this.adornerSvc = (AdornerService)this.component.Site.GetService(typeof(AdornerService));

            this.font = new Font("Tahoma", 8.0f);
            this.frameHeight = this.font.Height + 3 * BorderWidth + 7;
            this.frameWidth = 3 * this.frameHeight;

            this.textFormat = new StringFormat();
            this.textFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoWrap;
            this.textFormat.Trimming = StringTrimming.EllipsisCharacter;
            this.textFormat.Alignment = StringAlignment.Center;
            this.textFormat.LineAlignment = StringAlignment.Center;

            this.shapeFrame = new Frame(this, DockStyle.Right, Properties.Resources.Shape);
            this.pointFrame = new Frame(this, DockStyle.Top, Properties.Resources.Point);
            this.coordinateFrameX = new Frame(this, DockStyle.Bottom, Properties.Resources.XCoordinate);
            this.coordinateFrameY = new Frame(this, DockStyle.Left, Properties.Resources.YCoordinate);
            this.frames = new Frame[] { this.shapeFrame, this.pointFrame, this.coordinateFrameX, this.coordinateFrameY };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public AdornerService AdornerSvc
        {
            get
            {
                return this.adornerSvc;
            }

            set
            {
                this.adornerSvc = value;
            }
        }

        public override void OnShow()
        {
            this.component.ChangeService().ComponentChanged += new ComponentChangedEventHandler(this.OnComponentChanged);

            this.UpdateUI();
        }

        public override void OnHide()
        {
            this.component.ChangeService().ComponentChanged -= new ComponentChangedEventHandler(this.OnComponentChanged);
        }

        public override void Draw(Graphics gr)
        {
            if (gr == null)
            {
                throw new ArgumentNullException("gr");
            }

            for (int i = 0; i < this.frames.Length; i++)
            {
                this.frames[i].DoPaint(gr);
            }
        }

        public override void OnDragEnter(DragEventArgs de)
        {
            if (de == null)
            {
                throw new ArgumentNullException("de");
            }

            IFieldsDataObject dragFields = de.Data.GetData(typeof(IReportItemDataObject)) as IFieldsDataObject;

            de.Effect = dragFields != null ? DragDropEffects.Copy : DragDropEffects.None;
        }

        public override void OnDragOver(DragEventArgs de)
        {
            if (de == null)
            {
                throw new ArgumentNullException("de");
            }

            IFieldsDataObject dragFields = de.Data.GetData(typeof(IReportItemDataObject)) as IFieldsDataObject;
            de.Effect = dragFields != null ? DragDropEffects.Copy : DragDropEffects.None;

        }

        public override void OnDragDrop(DragEventArgs de)
        {
            if (de == null)
            {
                throw new ArgumentNullException("de");
            }

            IFieldsDataObject dragFields = de.Data.GetData(typeof(IReportItemDataObject)) as IFieldsDataObject;

            if (dragFields == null)
            {
                return;
            }

            de.Effect = DragDropEffects.Copy;

            this.DropField(this.GetHitFrame(this.adornerSvc.PointToAdorner(new Point(de.X, de.Y))), dragFields);
        }

        private static string FrameLabel(string Expression, string Default)
        {
            if (string.IsNullOrEmpty(Expression))
            {
                return Default;
            }

            return Expression;
        }

        private Frame GetHitFrame(Point pt)
        {
            Frame frameHit = null;

            for (int i = 0; i < this.frames.Length; i++)
            {
                if (this.frames[i].Bounds.Contains(pt))
                {
                    frameHit = this.frames[i];
                    break;
                }
            }

            return frameHit;
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (e.Component == this.component)
            {
                this.UpdateUI();
            }
        }

        private void UpdateUI()
        {
            Rectangle rect = this.adornerSvc.ComponentRectInDesignerFrame(this.component);

            this.bounds.Location = new Point(rect.X - this.frameWidth, rect.Y - this.frameHeight);
            this.bounds.Size = new Size(rect.Width + 2 * this.frameWidth + 1, rect.Height + 2 * this.frameHeight + 1);
            this.adornerSvc.AdornerWindowBounds = this.bounds;

            this.shapeFrame.Bounds = new Rectangle(this.frameWidth, 0, rect.Width, this.frameHeight);
            this.pointFrame.Bounds = new Rectangle(rect.Width + this.frameWidth, this.frameHeight, this.frameWidth, rect.Height);
            this.coordinateFrameY.Bounds = new Rectangle(0, this.frameHeight, this.frameWidth, rect.Height);
            this.coordinateFrameX.Bounds = new Rectangle(this.frameWidth, rect.Height + this.frameHeight, rect.Width, this.frameHeight);

            Region region = new Region(this.shapeFrame.Bounds);
            region.Union(this.pointFrame.Bounds);
            region.Union(this.coordinateFrameY.Bounds);
            region.Union(this.coordinateFrameX.Bounds);
            this.adornerSvc.AdornerWindowRegion = region;

            this.coordinateFrameX.Text = FrameLabel(this.component.XExpression, Properties.Resources.XCoordinate);
            this.coordinateFrameY.Text = FrameLabel(this.component.YExpression, Properties.Resources.YCoordinate);
            this.shapeFrame.Text = FrameLabel(this.component.ShapeExpression, Properties.Resources.Shape);
            this.pointFrame.Text = FrameLabel(this.component.PointExpression, Properties.Resources.Point);

            this.adornerSvc.InvalidateAdorner();
            region.Dispose();
        }

        private bool DropField(object state, IFieldsDataObject dragFields)
        {
            Frame frame = (Frame)state;
            Rdl.Field field = dragFields.Fields[0];

            this.component.ChangeService().OnComponentChanging(this.component, null);

            if (frame == this.coordinateFrameY)
            {
                this.component.YExpression = "=Fields!" + field.Name + ".Value";
            }
            else if (frame == this.coordinateFrameX)
            {
                this.component.XExpression = "=Fields!" + field.Name + ".Value";
            }
            else if (frame == this.shapeFrame)
            {
                this.component.ShapeExpression = "=Fields!" + field.Name + ".Value";
            }
            else if (frame == this.pointFrame)
            {
                this.component.PointExpression = "=Fields!" + field.Name + ".Value";
            }

            if (string.IsNullOrEmpty(this.component.DataSetName))
            {
                this.component.DataSetName = dragFields.DataSetName;
            }

            this.component.ChangeService().OnComponentChanged(this.component, null, null, null);
            return true;
        }

        private class Frame
        {
            private readonly PolygonsDesignWindow frameControl;
            private readonly DockStyle frameDock;
            private Rectangle frameBounds;
            private Size textSize;
            private string frameText;

            public Frame(PolygonsDesignWindow control, DockStyle dock, string text)
            {
                this.frameControl = control;
                this.frameDock = dock;
                this.frameText = text;
            }

            public Rectangle Bounds
            {
                get
                {
                    return this.frameBounds;
                }

                set
                {
                    this.frameBounds = value;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public PolygonsDesignWindow Control
            {
                get
                {
                    return this.frameControl;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public DockStyle Dock
            {
                get
                {
                    return this.frameDock;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public Size TextSize
            {
                get
                {
                    return this.textSize;
                }

                set
                {
                    this.textSize = value;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public string Text
            {
                get
                {
                    return this.frameText;
                }

                set
                {
                    this.frameText = value;

                    if (value != null)
                    {
                        using (Graphics gr = this.frameControl.AdornerSvc.AdornerWindowGraphics)
                        {
                            SizeF sizeF;

                            if (this.frameDock == DockStyle.Top || this.frameDock == DockStyle.Bottom)
                            {
                                sizeF = gr.MeasureString(value, this.frameControl.font);
                            }
                            else
                            {
                                sizeF = gr.MeasureString(
                                    value,
                                    this.frameControl.font,
                                    this.frameControl.frameWidth - 2 * BorderWidth,
                                    this.frameControl.textFormat);
                            }

                            this.textSize = new Size(
                                (int)Math.Ceiling(sizeF.Width),
                                (int)Math.Ceiling(sizeF.Height));
                        }
                    }
                }
            }

            public virtual void DoPaint(Graphics gr)
            {
                Rectangle rect = this.frameBounds;
                rect.Inflate(-BorderWidth / 2, -BorderWidth / 2);

                using (Pen pen = new Pen(SystemColors.Control, BorderWidth))
                {
                    gr.DrawRectangle(pen, rect);
                }

                Region clipRegion = gr.Clip;
                rect.Inflate(-BorderWidth / 2, -BorderWidth / 2);
                gr.IntersectClip(rect);

                if (this.frameText != null)
                {
                    using (Brush brush = new SolidBrush(SystemColors.WindowText))
                    {
                        StringFormat format = this.frameControl.textFormat;

                        if (this.frameDock == DockStyle.Top || this.frameDock == DockStyle.Bottom)
                        {
                            format.FormatFlags |= StringFormatFlags.NoWrap;
                        }
                        else
                        {
                            format.FormatFlags &= ~StringFormatFlags.NoWrap;
                        }

                        Rectangle TextRect = this.frameBounds;
                        TextRect.Inflate(-BorderWidth, -BorderWidth);
                        if (TextRect.Width > BorderWidth && TextRect.Height > 0)
                        {
                            gr.DrawString(this.frameText, this.frameControl.font, brush, TextRect, format);
                        }
                    }
                }

                gr.Clip = clipRegion;
            }
        }
    }
}
