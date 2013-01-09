// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuiButton.cs" company="Backdoor Fun">
//   © 2013
// </copyright>
// <summary>
//   Defines a button made for use with GDI+.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BlobDefense.Gui 
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Extensions;

    /// <summary>
    /// Defines a button made for use with GDI+.
    /// </summary>
    internal class GuiButton
    {
        /// <summary>
        /// The action to perform, when the button is clicked.
        /// </summary>
        private readonly Action clickAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiButton"/> class.
        /// </summary>
        /// <param name="positionAndSize"> A rectangle defining the position and size of the button. </param>
        /// <param name="standardImage"> The image used, when not interacting with the button. </param>
        /// <param name="hoverImage"> The image used, when the mouse is hovering over the button. </param>
        /// <param name="pressedImage"> The image used, when the buttons is pressed and hold. </param>
        /// <param name="clickAction"> The action to perform, when the button is clicked. </param>
        /// <param name="text"> The text to display on the button. </param>
        /// <param name="textFont"> The font used for the displayed text. </param>
        public GuiButton(Rectangle positionAndSize, Image standardImage, Image hoverImage = null, Image pressedImage = null, Action clickAction = null, string text = "", Font textFont = null)
        {
            // Initialize a new list if it is null
            AllButtons = AllButtons ?? new List<GuiButton>();
            
            // Assign fields
            this.Text = text;
            this.TextFont = textFont;
            this.StandardImage = standardImage;
            this.PositionAndSize = positionAndSize;
            this.clickAction = clickAction;
            this.TextColor = Color.Black;
            this.IsVisible = true;

            // If no hover or pressed image is supplied use the standard image
            this.HoverImage = hoverImage ?? standardImage;
            this.PressedImage = pressedImage ?? standardImage;

            // Add this button to the global button list
            AllButtons.Add(this);
        }

        /// <summary>
        /// The possible states of a button.
        /// </summary>
        public enum ButtonState
        {
            /// <summary>
            /// The buttons is not interacted with.
            /// </summary>
            Standard,

            /// <summary>
            /// The mouse is hovering over the button.
            /// </summary>
            Hovered,

            /// <summary>
            /// The button is being pressed.
            /// </summary>
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

        /// <summary>
        /// Invokes the click action, if the button is visible.
        /// </summary>
        public void Click()
        {
            // Return if the button is not visible
            if (!this.IsVisible)
            {
                return;
            }
            
            this.clickAction.SafeInvoke();
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="graphics">
        /// The graphics context used to draw the button.
        /// </param>
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
