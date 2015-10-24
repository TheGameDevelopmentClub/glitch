using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace glitch.Physics
{
    public enum HitboxHit
    {
        None, Left, Top, Right, Bottom
    }

    public class Hitbox
    {
        public Rectangle overall, left, right, top, bottom;
        public Point leftOffset, rightOffset, topOffset, bottomOffset;
        public int threshold;

        public Hitbox(int width, int height)
        {
            overall = new Rectangle();
            left = new Rectangle();
            right = new Rectangle();
            top = new Rectangle();
            bottom = new Rectangle();
            leftOffset = new Point();
            rightOffset = new Point();
            topOffset = new Point();
            bottomOffset = new Point();

            this.ReSize(width, height);
        }

        public void ReSize(int width, int height)
        {
            this.ReSize(new Point(width, height));
        }

        public void ReSize(Point size)
        {
            overall.Size = size;
            left.Size = new Point(size.X / 2, size.Y / 3);
            right.Size = new Point(size.X / 2, size.Y / 3);
            top.Size = new Point(size.X, size.Y / 3);
            bottom.Size = new Point(size.X, size.Y / 3);

            leftOffset = new Point(0, size.Y/ 3);
            rightOffset = new Point(size.X/ 2, size.Y/ 3);
            topOffset = Point.Zero;
            bottomOffset = new Point(0, (2 * size.X) / 3);

            threshold = (int) Math.Pow(Math.Max(overall.Width, overall.Height), 2);

            this.UpdatePosition(overall.Location);
        }

        public void UpdatePosition(Point pos)
        {
            overall.Location = pos;
            left.Location = pos + leftOffset;
            right.Location = pos + rightOffset;
            top.Location = pos + topOffset;
            bottom.Location = pos + bottomOffset;
        }

        public void UpdatePosition(int x, int y)
        {
            this.UpdatePosition(new Point(x, y));
        }

        public bool isTouching(Hitbox oppHitbox)
        {
            return this.overall.Intersects(oppHitbox.overall);
        }

        public bool isNearby(Hitbox oppHitBox)
        {

            int maxThreshold = Math.Max(this.threshold, oppHitBox.threshold);

            Vector2 diffVector = oppHitBox.overall.Location.ToVector2() - this.overall.Location.ToVector2();

            return diffVector.LengthSquared() < maxThreshold;
        }

        public HitboxHit Intersects(Hitbox oppHitbox)
        {
            Rectangle rect = oppHitbox.overall;

            if (overall.Intersects(rect))
            {
                if (bottom.Intersects(rect))
                {
                    return HitboxHit.Bottom;
                }
                else if (left.Intersects(rect))
                {
                    return HitboxHit.Left;
                }
                else if (right.Intersects(rect))
                {
                    return HitboxHit.Right;
                }
                else if (top.Intersects(rect))
                {
                    return HitboxHit.Top;
                }
            }
            return HitboxHit.None;
        }

    }
}
