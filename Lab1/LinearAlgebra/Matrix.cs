using System;
using System.Collections;

namespace LinearAlgebra
{
	public class IncorrectMatrixSizesException : Exception { }

	public class Matrix
	{
		public int RowNumber { get; private set; }
		public int ColumnNumber { get; private set; }
		int[,] _matrix;

		public Matrix(int rowNumber, int columnNumber)
		{
			RowNumber = rowNumber;
			ColumnNumber = columnNumber;
			_matrix = new int[RowNumber, ColumnNumber];
		}

        public Matrix GetTransposed()
        {
            var result = new Matrix(ColumnNumber, RowNumber);

            for (int i = 0; i < RowNumber; ++i)
                for (int j = 0; j < ColumnNumber; ++j)
                    result[j, i] = _matrix[i, j];

            return result;
        }

		public static Matrix operator +(Matrix leftHandSide, Matrix rightHandSide)
		{
			if (leftHandSide.RowNumber != rightHandSide.RowNumber || leftHandSide.ColumnNumber != rightHandSide.ColumnNumber)
				throw new IncorrectMatrixSizesException();

			var result = new Matrix(leftHandSide.RowNumber, rightHandSide.ColumnNumber);

			for (int i = 0; i < leftHandSide.RowNumber; ++i)
				for (int j = 0; j < rightHandSide.ColumnNumber; ++j)
					result[i, j] = leftHandSide[i, j] + rightHandSide[i, j];

			return result;
		}

		public static Matrix operator *(Matrix leftHandSide, Matrix rightHandSide)
		{
			if (leftHandSide.ColumnNumber != rightHandSide.RowNumber)
				throw new IncorrectMatrixSizesException();

			var result = new Matrix(leftHandSide.RowNumber, rightHandSide.ColumnNumber);

			for (int i = 0; i < leftHandSide.RowNumber; ++i)
				for (int j = 0; j < rightHandSide.ColumnNumber; ++j)
					for (int k = 0; k < leftHandSide.ColumnNumber; ++k)
						result[i, j] += leftHandSide[i, k] * rightHandSide[k, j];

			return result;
		}

		public static Matrix operator %(Matrix matrix, int module)
		{
			var result = new Matrix(matrix.RowNumber, matrix.ColumnNumber);

			for (int i = 0; i < matrix.RowNumber; ++i)
				for (int j = 0; j < matrix.ColumnNumber; ++j)
				{
					result[i, j] = matrix[i, j] % module;
					if (result[i, j] < 0)
						result[i, j] += module;
				}

			return result;
		}

		public int this[int rowIndex, int columnIndex]
		{
			get { return _matrix[rowIndex, columnIndex]; }
			set { _matrix[rowIndex, columnIndex] = value; }
		}

		public ColumnVector this[int index]
		{
			get 
			{
				var result = new ColumnVector(RowNumber);
				for (int i = 0; i < RowNumber; ++i)
					result[i] = _matrix[i, index];
				return result;
			}
			set 
			{
				for (int i = 0; i < RowNumber; ++i)
					_matrix[i, index] = value[i];
			}
		}
	}
}
