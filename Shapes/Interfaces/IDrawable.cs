using System.Drawing;

namespace Shapes.Interfaces
{
    interface IDrawable
    {
        void Draw(Graphics graphics, Pen pen);
    }
}
