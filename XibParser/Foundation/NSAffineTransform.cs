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
        new public static NSAffineTransform Alloc() { return new NSAffineTransform(); }

        NSAffineTransformStruct _matrix;
        bool _isIdentity;	// special case: A=D=1 and B=C=0
        bool _isFlipY;	// special case: A=1 D=-1 and B=C=0


        private static NSAffineTransformStruct _IdentityTransform = new NSAffineTransformStruct(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
        public static NSAffineTransformStruct IdentityTransform
        {
            get { return _IdentityTransform; }
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

        public override NSString Description()
        {
            return NSString.StringWithFormat(@"NSAffineTransform ((%f, %f) (%f, %f) (%f, %f))",
                _matrix.m11, _matrix.m12, _matrix.m21, _matrix.m22, _matrix.tX, _matrix.tY);
        }

        public override id Init()
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

       

        public virtual void MakeIdentityMatrix()
        {
            Init();
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
                NSLog.Log(@"error: determinant of matrix is 0!");
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

    }
}
