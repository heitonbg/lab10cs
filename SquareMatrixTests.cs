using System;
using Xunit;

public class SquareMatrixTests
{
  [Fact]
  public void Constructor_WithValidSize_CreatesMatrix()
  {
    var matrix = new SquareMatrix(3);

    Assert.Equal(3, matrix.GetSize());
  }

  [Fact]
  public void Constructor_WithArray_CreatesMatrix()
  {
    int[,] testArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

    var matrix = new SquareMatrix(testArray);

    Assert.Equal(testArray, matrix.GetMatrix());
  }

  [Fact]
  public void Constructor_WithNonSquareArray_ThrowsException()
  {
    int[,] invalidArray = { { 1, 2 }, { 3, 4 }, { 5, 6 } };

    Assert.Throws<ArgumentException>(() => new SquareMatrix(invalidArray));
  }

  [Fact]
  public void AddOperator_WithSameSize_ReturnsSum()
  {
    var firstMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });
    var secondMatrix = new SquareMatrix(new int[,] { { 5, 6 }, { 7, 8 } });

    var result = firstMatrix + secondMatrix;

    Assert.Equal(new int[,] { { 6, 8 }, { 10, 12 } }, result.GetMatrix());
  }

  [Fact]
  public void AddOperator_WithDifferentSizes_ThrowsException()
  {
    var firstMatrix = new SquareMatrix(2);
    var secondMatrix = new SquareMatrix(3);

    Assert.Throws<MatrixSizeMismatchException>(() => firstMatrix + secondMatrix);
  }

  [Fact]
  public void MultiplyOperator_WithSameSize_ReturnsProduct()
  {
    var firstMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });
    var secondMatrix = new SquareMatrix(new int[,] { { 5, 6 }, { 7, 8 } });

    var result = firstMatrix * secondMatrix;

    Assert.Equal(new int[,] { { 19, 22 }, { 43, 50 } }, result.GetMatrix());
  }

  [Fact]
  public void MultiplyOperator_WithDifferentSizes_ThrowsException()
  {
    var firstMatrix = new SquareMatrix(2);
    var secondMatrix = new SquareMatrix(3);

    Assert.Throws<MatrixSizeMismatchException>(() => firstMatrix * secondMatrix);
  }

  [Fact]
  public void Determinant_2x2Matrix_CalculatesCorrectly()
  {
    var matrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });

    var determinant = matrix.Determinant();

    Assert.Equal(-2, determinant);
  }

  [Fact]
  public void Determinant_3x3Matrix_CalculatesCorrectly()
  {
    var matrix = new SquareMatrix(new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

    var determinant = matrix.Determinant();

    Assert.Equal(0, determinant);
  }

  [Fact]
  public void Inverse_2x2Matrix_CalculatesCorrectly()
  {
    var matrix = new SquareMatrix(new int[,] { { 4, 7 }, { 2, 6 } });

    var inverseMatrix = matrix.Inverse();

    var expectedMatrix = new SquareMatrix(new int[,] { { 6, -7 }, { -2, 4 } });
    Assert.Equal(expectedMatrix.GetMatrix(), inverseMatrix.GetMatrix());
  }

  [Fact]
  public void Inverse_SingularMatrix_ThrowsException()
  {
    var matrix = new SquareMatrix(new int[,] { { 1, 2 }, { 2, 4 } });

    Assert.Throws<InvalidMatrixOperationException>(() => matrix.Inverse());
  }

  [Fact]
  public void CompareOperators_CompareByDeterminant()
  {
    var firstMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });
    var secondMatrix = new SquareMatrix(new int[,] { { 0, 1 }, { 1, 0 } });

    Assert.True(firstMatrix < secondMatrix);
    Assert.False(firstMatrix > secondMatrix);
  }

  [Fact]
  public void EqualityOperators_CompareMatrixValues()
  {
    var firstMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });
    var secondMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });
    var thirdMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 5 } });

    Assert.True(firstMatrix == secondMatrix);
    Assert.False(firstMatrix == thirdMatrix);
    Assert.True(firstMatrix != thirdMatrix);
  }

  [Fact]
  public void ExplicitConversion_ToInt_ReturnsDeterminant()
  {
    var matrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });

    int determinant = (int)matrix;

    Assert.Equal(-2, determinant);
  }

  [Fact]
  public void TrueFalseOperators_CheckDeterminant()
  {
    var singularMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 2, 4 } });
    var nonSingularMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });

    Assert.Equal(0, singularMatrix.Determinant());
    Assert.NotEqual(0, nonSingularMatrix.Determinant());
  }

  [Fact]
  public void Clone_CreatesDeepCopy()
  {
    var originalMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });

    var clonedMatrix = originalMatrix.Clone() as SquareMatrix;

    Assert.Equal(originalMatrix.GetMatrix(), clonedMatrix.GetMatrix());
    Assert.NotSame(originalMatrix.GetMatrix(), clonedMatrix.GetMatrix());
  }

  [Fact]
  public void CompareTo_ComparesByDeterminant()
  {
    var firstMatrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });
    var secondMatrix = new SquareMatrix(new int[,] { { 0, 1 }, { 1, 0 } });

    Assert.Equal(-1, firstMatrix.CompareTo(secondMatrix));
    Assert.Equal(1, secondMatrix.CompareTo(firstMatrix));
  }

  [Fact]
  public void ToString_ReturnsCorrectFormat()
  {
    var matrix = new SquareMatrix(new int[,] { { 1, 2 }, { 3, 4 } });

    var resultString = matrix.ToString();

    Assert.Equal("1 2 \n3 4 \n", resultString);
  }
}

public static class SquareMatrixTestExtensions
{
  public static int[,] GetMatrix(this SquareMatrix matrix)
  {
    var matrixField = typeof(SquareMatrix).GetField("_matrix",
      System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    return (int[,])matrixField.GetValue(matrix);
  }

  public static int GetSize(this SquareMatrix matrix)
  {
    var sizeField = typeof(SquareMatrix).GetField("_size",
      System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    return (int)sizeField.GetValue(matrix);
  }
}