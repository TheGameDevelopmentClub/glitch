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
        public Vector2 leftOffset, rightOffset, topOffset, bottomOffset;
        public int threshold;
        public Vector2 position;

        public Hitbox(int width, int height)
        {
            overall = new Rectangle();
            left = new Rectangle();
            right = new Rectangle();
            top = new Rectangle();
            bottom = new Rectangle();
            leftOffset = new Vector2();
            rightOffset = new Vector2();
            topOffset = new Vector2();
            bottomOffset = new Vector2();
            position = new Vector2();

            this.ReSize(width, height);
        }

        public void ReSize(int width, int height)
        {
            this.ReSize(new Vector2(width, height));
        }

        public void ReSize(Vector2 size)
        {
            Point sizeAsPoint = size.ToPoint();
            overall.Size = sizeAsPoint;
            left.Size = new Point(sizeAsPoint.X / 3, sizeAsPoint.Y / 3);
            right.Size = new Point(sizeAsPoint.X / 3, sizeAsPoint.Y / 3);
            top.Size = new Point(sizeAsPoint.X - 6, sizeAsPoint.Y / 3);
            bottom.Size = new Point(sizeAsPoint.X - 6, sizeAsPoint.Y / 3);

            leftOffset = new Vector2(0, (overall.Height - left.Height) / 2);
            rightOffset = new Vector2((overall.Width - right.Width), (overall.Height - right.Height) / 2);
            topOffset = new Vector2(3, 0);
            bottomOffset = new Vector2(3, size.Y - bottom.Height);

            threshold = (int) Math.Pow(Math.Max(overall.Width, overall.Height), 2);

            this.UpdatePosition(overall.Location.ToVector2());
        }

        public void UpdatePosition(Vector2 pos)
        {
            this.position = pos;
            overall.Location = position.ToPoint();
            left.Location = (position + leftOffset).ToPoint();
            right.Location = (position + rightOffset).ToPoint();
            top.Location = (position + topOffset).ToPoint();
            bottom.Location = (position + bottomOffset).ToPoint();
        }

        public void UpdatePosition(int x, int y)
        {
            this.UpdatePosition(new Vector2(x, y));
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
                if (left.Intersects(rect))
                {
                    return HitboxHit.Left;
                }
                else if (right.Intersects(rect))
                {
                    return HitboxHit.Right;
                }
                else if (bottom.Intersects(rect))
                {
                    return HitboxHit.Bottom;
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
