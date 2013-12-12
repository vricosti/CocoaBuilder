using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/Foundation/Classes/NSAffineTransform_Class/Reference/Reference.html
    
    public struct NSAffineTransformStruct 
    {
        public double m11, m12, m21, m22;
        public double tX, tY;

        public NSAffineTransformStruct(double m11, double m12, double m21, double m22, double tX, double tY)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.tX = tX;
            this.tY = tY;
        }
    }

    //https://github.com/gnustep/gnustep-base/blob/master/Headers/Foundation/NSAffineTransform.h
    //https://github.com/gnustep/gnustep-base/blob/master/Source/NSAffineTransform.m
    public class NSAffineTransform : NSObject
    {
        new public static Class Class = new Class(typeof(NSAffineTransform));
        new public static NSAffineTransform alloc() { return new NSAffineTransform(); }

        NSAffineTransformStruct _matrix;
        bool _isIdentity;	// special case: A=D=1 and B=C=0
        bool _isFlipY;	// special case: A=1 D=-1 and B=C=0


        private static NSAffineTransformStruct _IdentityTransform = new NSAffineTransformStruct(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
        public static NSAffineTransformStruct IdentityTransform
        {
            get { return _IdentityTransform; }
        }

		public static NSAffineTransform Transform
		{
			get { return GetTransform (); }
		}

		public static NSAffineTransform GetTransform()
		{
			NSAffineTransform	t;

			t = (NSAffineTransform)NSAffineTransform.alloc().init();
			t._matrix = IdentityTransform;
			t._isIdentity = true;
			return t;
		}

        private static NSAffineTransformStruct matrix_multiply(NSAffineTransformStruct MA, NSAffineTransformStruct MB)
        {
            NSAffineTransformStruct MC;
            MC.m11 = MA.m11 * MB.m11 + MA.m12 * MB.m21;
            MC.m12 = MA.m11 * MB.m12 + MA.m12 * MB.m22;
            MC.m21 = MA.m21 * MB.m11 + MA.m22 * MB.m21;
            MC.m22 = MA.m21 * MB.m12 + MA.m22 * MB.m22;
            MC.tX = MA.tX * MB.m11 + MA.tY * MB.m21 + MB.tX;
            MC.tY = MA.tX * MB.m12 + MA.tY * MB.m22 + MB.tY;
            return MC;
        }

        public override NSString description()
        {
            return NSString.stringWithFormat(@"NSAffineTransform ((%f, %f) (%f, %f) (%f, %f))",
                _matrix.m11, _matrix.m12, _matrix.m21, _matrix.m22, _matrix.tX, _matrix.tY);
        }

        public override id init()
        {
            id self = this;

            _matrix = IdentityTransform;
            _isIdentity = true;
            
            return self;
        }

        public virtual id InitWithTransform(ref NSAffineTransform aTransform)
        {
            id self = this;
            _matrix = aTransform._matrix;
            _isIdentity = aTransform._isIdentity;
            _isFlipY = aTransform._isFlipY;
            return self;
        }

       public virtual id Copy()
		{
			NSAffineTransform transform = new NSAffineTransform();
			transform._matrix = this._matrix;
			transform._isIdentity = this._isIdentity;
			transform._isFlipY = this._isFlipY;

			return transform;
		}

        public virtual void MakeIdentityMatrix()
        {
            init();
        }



        public virtual void Invert()
        {
            double newA, newB, newC, newD, newTX, newTY;
            double det;

            if (_isIdentity)
            {
                _matrix.tX = -_matrix.tX;
                _matrix.tY = -_matrix.tY;
                return;
            }

            if (_isFlipY)
            {
                _matrix.tX = -_matrix.tX;
                return;
            }

            det = _matrix.m11 * _matrix.m22 - _matrix.m12 * _matrix.m21;
            if (det == 0)
            {
                NSLog.log(@"error: determinant of matrix is 0!");
                return;
            }

            newA = _matrix.m22 / det;
            newB = -_matrix.m12 / det;
            newC = -_matrix.m21 / det;
            newD = _matrix.m11 / det;
            newTX = (-_matrix.m22 * _matrix.tX + _matrix.m21 * _matrix.tY) / det;
            newTY = (_matrix.m12 * _matrix.tX - _matrix.m11 * _matrix.tY) / det;

            //NSDebugLLog(@"NSAffineTransform",
            //  @"inverse of matrix ((%f, %f) (%f, %f) (%f, %f))\n"
            //  @"is ((%f, %f) (%f, %f) (%f, %f))",
            //  _matrix.m11, _matrix.m12, _matrix.m21, _matrix.m22, _matrix.tX, _matrix.tY,
            //  newA, newB, newC, newD, newTX, newTY);

            _matrix.m11 = newA; _matrix.m12 = newB;
            _matrix.m21 = newC; _matrix.m22 = newD;
            _matrix.tX = newTX; _matrix.tY = newTY;
        }

        public virtual void PrependTransform(ref NSAffineTransform aTransform)
        {
            //valid(aTransform);

            if (aTransform._isIdentity)
            {
                double newTX;

                newTX = aTransform._matrix.tX * _matrix.m11 + aTransform._matrix.tY * _matrix.m21 + _matrix.tX;
                _matrix.tY = aTransform._matrix.tX * _matrix.m12 + aTransform._matrix.tY * _matrix.m22 + _matrix.tY;
                _matrix.tX = newTX;
                //check();
                return;
            }

            if (aTransform._isFlipY)
            {
                double newTX;

                newTX = aTransform._matrix.tX * _matrix.m11 + aTransform._matrix.tY * _matrix.m21 + _matrix.tX;
                _matrix.tY = aTransform._matrix.tX * _matrix.m12 + aTransform._matrix.tY * _matrix.m22 + _matrix.tY;
                _matrix.tX = newTX;
                _matrix.m21 = -_matrix.m21;
                _matrix.m22 = -_matrix.m22;
                if (_isIdentity)
                {
                    _isFlipY = true;
                    _isIdentity = false;
                }
                else if (_isFlipY)
                {
                    _isFlipY = false;
                    _isIdentity = true;
                }
                //check();
                return;
            }

            if (_isIdentity)
            {
                _matrix.m11 = aTransform._matrix.m11;
                _matrix.m12 = aTransform._matrix.m12;
                _matrix.m21 = aTransform._matrix.m21;
                _matrix.m22 = aTransform._matrix.m22;
                _matrix.tX += aTransform._matrix.tX;
                _matrix.tY += aTransform._matrix.tY;
                _isIdentity = false;
                _isFlipY = aTransform._isFlipY;
                //check();
                return;
            }

            if (_isFlipY)
            {
                _matrix.m11 = aTransform._matrix.m11;
                _matrix.m12 = -aTransform._matrix.m12;
                _matrix.m21 = aTransform._matrix.m21;
                _matrix.m22 = -aTransform._matrix.m22;
                _matrix.tX += aTransform._matrix.tX;
                _matrix.tY -= aTransform._matrix.tY;
                _isIdentity = false;
                _isFlipY = false;
                //check();
                return;
            }

            _matrix = matrix_multiply(aTransform._matrix, _matrix);
            _isIdentity = false;
            _isFlipY = false;
            //check();
        }

        public virtual void RotateByDegrees(double angle)
        {
            this.RotateByRadians(Math.PI * angle / 180);
        }

        public virtual void RotateByRadians(double angleRad)
        {
            if (angleRad != 0.0)
            {
                double sine;
                double cosine;
                NSAffineTransformStruct rotm;

                sine = Math.Sin(angleRad);
                cosine = Math.Cos(angleRad);
                rotm.m11 = cosine;
                rotm.m12 = sine;
                rotm.m21 = -sine;
                rotm.m22 = cosine;
                rotm.tX = rotm.tY = 0;
                _matrix = matrix_multiply(rotm, _matrix);
                _isIdentity = false;
                _isFlipY = false;
                //check();
            }
        }

		public virtual void SetFrameOrigin(NSPoint point)
		{
			NSAffineTransformStruct matrix = this.GetTransformStruct();
			double dx = point.X - _matrix.tX;
			double dy = point.Y - _matrix.tY;

			this.TranslateXByYBy(dx,dy);
		}

		public virtual void SetFrameRotation(double angle)
		{
			this.RotateByDegrees(angle - this.RotationAngle());
		}

		public virtual double RotationAngle()
		{
			NSAffineTransformStruct matrix = this.GetTransformStruct();
			double rotationAngle = Math.Atan2(-_matrix.m21, _matrix.m11);

			rotationAngle *= 180.0 / Math.PI;
			if (rotationAngle < 0.0)
				rotationAngle += 360.0;

			return rotationAngle;
		}

		public virtual void ScaleTo(double sx, double sy)
		{
			NSAffineTransformStruct matrix = this.GetTransformStruct();

			/* If it's rotated.  */
			if (_matrix.m12 != 0  ||  _matrix.m21 != 0)
			{
				// FIXME: This case does not handle shear.
				double angle = this.RotationAngle();

				// Keep the translation and add scaling
				_matrix.m11 = sx; _matrix.m12 = 0;
				_matrix.m21 = 0; _matrix.m22 = sy;
				this.SetTransformStruct(matrix);

				// Prepend the rotation to the scaling and translation
				this.RotateByDegrees(angle);
			}
			else
			{
				_matrix.m11 = sx; _matrix.m12 = 0;
				_matrix.m21 = 0; _matrix.m22 = sy;
				this.SetTransformStruct(matrix);
			}
		}

        public virtual void ScaleBy(double scale)
        {
            NSAffineTransformStruct scam = IdentityTransform;

            scam.m11 = scale;
            scam.m22 = scale;
            _matrix = matrix_multiply(scam, _matrix);
            _isIdentity = false;
            _isFlipY = false;
            //check();
        }

        public virtual void ScaleXByYBy(double scaleX, double scaleY)
        {
            if (_isIdentity && scaleX == 1.0)
            {
                if (scaleY == 1.0)
                {
                    return;	// no scaling
                }
                if (scaleY == -1.0)
                {
                    _matrix.m22 = -1.0;
                    _isFlipY = true;
                    _isIdentity = false;
                    return;
                }
            }

            if (_isFlipY && scaleX == 1.0)
            {
                if (scaleY == 1.0)
                {
                    return;	// no scaling
                }
                if (scaleY == -1.0)
                {
                    _matrix.m22 = 1.0;
                    _isFlipY = false;
                    _isIdentity = true;
                    return;
                }
            }

            _matrix.m11 *= scaleX;
            _matrix.m12 *= scaleX;
            _matrix.m21 *= scaleY;
            _matrix.m22 *= scaleY;
            _isIdentity = false;
            _isFlipY = false;
        }

        public virtual void SetTransformStruct(NSAffineTransformStruct val)
        {
            _matrix = val;
            _isIdentity = false;
            _isFlipY = false;
            if (_matrix.m11 == 1.0 && _matrix.m12 == 0.0 && _matrix.m21 == 0.0)
            {
                if (_matrix.m22 == 1.0)
                {
                    _isIdentity = true;
                }
                else if (_matrix.m22 == -1.0)
                {
                    _isFlipY = true;
                }
            }
            //check();
        }

        public virtual NSPoint TransformPoint(NSPoint aPoint)
        {
            NSPoint newPoint = NSPoint.Zero;

            if (_isIdentity)
            {
                newPoint.X = _matrix.tX + aPoint.X;
                newPoint.Y = _matrix.tY + aPoint.Y;
            }
            else if (_isFlipY)
            {
                newPoint.X = _matrix.tX + aPoint.X;
                newPoint.Y = _matrix.tY - aPoint.Y;
            }
            else
            {
                newPoint.X = _matrix.m11 * aPoint.X + _matrix.m21 * aPoint.Y + _matrix.tX;
                newPoint.Y = _matrix.m12 * aPoint.X + _matrix.m22 * aPoint.Y + _matrix.tY;
            }

            return newPoint;
        }

        public virtual NSSize TransformSize(NSSize aSize)
        {
            if (_isIdentity)
            {
                return aSize;
            }
            else
            {
                NSSize newSize = new NSSize(0, 0);

                if (_isFlipY)
                {
                    newSize.Width = aSize.Width;
                    newSize.Height = -aSize.Height;
                }
                else
                {
                    newSize.Width = _matrix.m11 * aSize.Width + _matrix.m21 * aSize.Height;
                    newSize.Height = _matrix.m12 * aSize.Width + _matrix.m22 * aSize.Height;
                }
                return newSize;
            }
        }

        public virtual NSAffineTransformStruct GetTransformStruct()
        {
            return _matrix;
        }

		public virtual void TranslateXByYBy(double tranX, double tranY)
		{
			if (_isIdentity)
			{
				_matrix.tX += tranX;
				_matrix.tY += tranY;
			}
			else if (_isFlipY)
			{
				_matrix.tX += tranX;
				_matrix.tY -= tranY;
			}
			else
			{
				_matrix.tX += _matrix.m11 * tranX + _matrix.m21 * tranY;
				_matrix.tY += _matrix.m12 * tranX + _matrix.m22 * tranY;
			}
			//check();
		}

		public virtual void BoundingRectFor(NSRect rect, ref  NSRect newRect)
		{
			NSAffineTransformStruct matrix = this.GetTransformStruct();
			/* Shortcuts of the usual rect values */
			double x = rect.Origin.X;
			double y = rect.Origin.Y;
			double width = rect.Size.Width;
			double height = rect.Size.Height;
			double[] xc = new double[3];
			double[] yc = new double[3];
			double min_x;
			double max_x;
			double min_y;
			double max_y;
			int i;

			max_x = _matrix.m11 * x + _matrix.m21 * y + _matrix.tX;
			max_y = _matrix.m12 * x + _matrix.m22 * y + _matrix.tY;
			xc[0] = max_x + _matrix.m11 * width;
			yc[0] = max_y + _matrix.m12 * width;
			xc[1] = max_x + _matrix.m21 * height;
			yc[1] = max_y + _matrix.m22 * height;
			xc[2] = max_x + _matrix.m11 * width + _matrix.m21 * height;
			yc[2] = max_y + _matrix.m12 * width + _matrix.m22 * height;

			min_x = max_x;
			min_y = max_y;

			for (i = 0; i < 3; i++) 
			{
				if (xc[i] < min_x)
					min_x = xc[i];
				if (xc[i] > max_x)
					max_x = xc[i];

				if (yc[i] < min_y)
					min_y = yc[i];
				if (yc[i] > max_y)
					max_y = yc[i];
			}

            newRect = NSRect.Make(min_x, min_y, max_x - min_x, max_y - min_y);
            //newRect.Origin.X = min_x;
            //newRect.Origin.Y = min_y;
            //newRect.Size.Width = max_x -min_x;
            //newRect.Size.Height = max_y -min_y;
		}
    }
}
