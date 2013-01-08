using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobDefense.Gui 
{
    using System.Drawing;

    using Extensions;

    internal class GuiButton
    {
        private Action clickAction;
        
        public GuiButton(Rectangle positionAndSize, Image standardImage, Image hoverImage = null, Image pressedImage = null, Action clickAction = null, string text = "", Font textFont = null)
        {
            // Initialize a new list if it is null
            AllButtons = AllButtons ?? new List<GuiButton>();
            
            this.Text = text;
            this.TextFont = textFont;
            this.StandardImage = standardImage;
            this.PositionAndSize = positionAndSize;
            this.clickAction = clickAction;
            this.TextColor = Color.Black;
            this.IsVisible = true;

            this.HoverImage = hoverImage ?? standardImage;
            this.PressedImage = pressedImage ?? standardImage;

            // Add this button to the global button list
            AllButtons.Add(this);
        }

        public enum ButtonState
        {
            Standard,
            Hovered,
            Pressed
        }

        /// <summary>
        /// Gets a list of all the buttons created.
        /// </summary>
        public static List<GuiButton> AllButtons { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the button is visible or not.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the text displayed on the button.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the current state of the button.
        /// </summary>
        public ButtonState State { get; set; }

        /// <summary>
        /// Gets the font for the text displayed on the button.
        /// </summary>
        public Font TextFont { get; private set; }

        /// <summary>
        /// Gets the color for the text displayed on the button.
        /// </summary>
        public Color TextColor { get; private set; }

        /// <summary>
        /// Gets the position and size rectangle for the button.
        /// </summary>
        public Rectangle PositionAndSize { get; private set; }

        /// <summary>
        /// Gets the image displayed on the button as standard.
        /// </summary>
        public Image StandardImage { get; private set; }

        /// <summary>
        /// Gets the image displayed on the button when hovered.
        /// </summary>
        public Image HoverImage { get; private set; }

        /// <summary>
        /// Gets the image displayed on the button when pressed.
        /// </summary>
        public Image PressedImage { get; private set; }

        public void Click()
        {
            // Return if the button is not visible
            if (!this.IsVisible)
            {
                return;
            }
            
            this.clickAction.SafeInvoke();
        }

        public void Draw(Graphics graphics)
        {
            // Return if the button is not visible
            if (!this.IsVisible)
            {
                return;
            }
            
            // Draw button image
            switch (this.State)
            {
                case ButtonState.Standard:
                    graphics.DrawImageUnscaledAndClipped(this.StandardImage, this.PositionAndSize);
                    break;
                case ButtonState.Hovered:
                    graphics.DrawImageUnscaledAndClipped(this.HoverImage, this.PositionAndSize);
                    break;
                case ButtonState.Pressed:
                    graphics.DrawImageUnscaledAndClipped(this.PressedImage, this.PositionAndSize);
                    break;
            }

            // Draw text if any
            if (this.Text != string.Empty)
            {
                graphics.DrawString(this.Text, this.TextFont, new SolidBrush(this.TextColor), this.PositionAndSize);
            }
        }
    }
}
