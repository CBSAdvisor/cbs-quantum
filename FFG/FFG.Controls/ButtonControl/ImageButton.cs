using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFG.Controls.ButtonControl
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Button)), ToolboxItem(true), 
    ToolboxItemFilter("System.Windows.Forms"), Description("Displays an button with images.")]
    public partial class ImageButton : Button
    {
        private bool _hover = false;
        private bool _down = false;
        //private bool _isDefault = false;

        public ImageButton()
            : base()
        {
            InitializeComponent();
            //this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderSize = 0;
            //this.BackColor = Color.Transparent;
        }

        #region HoverImage
        private Image _hoverImage;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Image to show when the button is hovered over.")]
        public Image HoverImage
        {
            get { return _hoverImage; }
            set { _hoverImage = value; if (_hover) Image = value; }
        }
        #endregion
        #region DownImage
        private Image _downImage;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Image to show when the button is depressed.")]
        public Image DownImage
        {
            get { return _downImage; }
            set { _downImage = value; if (_down) Image = value; }
        }
        #endregion
        #region NormalImage
        private Image _normalImage;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Image to show when the button is not in any other state.")]
        public Image NormalImage
        {
            get { return _normalImage; }
            set { _normalImage = value; if (!(_hover || _down)) Image = value; }
        }
        #endregion

        #region Hiding
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image Image { get { return base.Image; } set { base.Image = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int ImageIndex { get { return base.ImageIndex; } set { base.ImageIndex = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ImageKey { get { return base.ImageKey; } set { base.ImageKey = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageList ImageList { get { return base.ImageList; } set { base.ImageList = value; } }

        #endregion

        #region Events
        protected override void OnMouseMove(MouseEventArgs e)
        {
            _hover = true;
            if (_down)
            {
                if ((DownImage != null) && (Image != DownImage))
                    Image = DownImage;
            }
            else
                if (HoverImage != null)
                    Image = HoverImage;
                else
                    Image = NormalImage;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hover = false;
            Image = NormalImage;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.Focus();
            OnMouseUp(null);
            _down = true;
            if (DownImage != null)
                Image = DownImage;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _down = false;
            if (_hover)
            {
                if (HoverImage != null)
                    Image = HoverImage;
            }
            else
                Image = NormalImage;
            base.OnMouseUp(e);
        }
        #endregion
    }
}
