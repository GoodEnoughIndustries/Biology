using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Structural
{
    /// <summary>
    /// Collection that represents a layer of <typeparamref name="T"/> - cell, etc.
    /// </summary>
    /// <typeparam name="T">Type that is in a layer.</typeparam>
    public class Layer<T> : IDisposable
    {
        private bool disposedValue;

        private T[][] grid;

        public Layer(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.grid = new T[this.Width][];

            for (var i = 0; i < this.Width; i++)
            {
                this.grid[i] = new T[this.Height];
            }
        }

        public int Count => this.Width * this.Height;

        public int Width { get; init; }
        public int Height { get; init; }
        public Action<int, int, T> Changed { get; set; } = (x, y, t) => { };

        public void SetRange(Range columnRange, T value)
        {
            if (value is null)
            {
                throw new NullReferenceException();
            }

            var (Offset, Length) = columnRange.GetOffsetAndLength(this.grid.Length);
            for (var i = 0; i < Length; i++)
            {
                var column = this.grid[i + Offset];
                for (var y = 0; y < column.Length; y++)
                {
                    column[y] = value;
                    this.Changed(i + Offset, y, value);
                }
            }
        }

        public void SetRange(Range columnRange, T[] value)
        {
            if (value is null)
            {
                throw new NullReferenceException();
            }

            var (Offset, Length) = columnRange.GetOffsetAndLength(this.grid.Length);
            for (var i = 0; i < Length; i++)
            {
                var column = this.grid[i + Offset];// = value[i];
                var shortest = Math.Min(column.Length, value.Length);
                for (var y = 0; y < shortest; y++)
                {
                    column[y] = value[y];
                    this.Changed(i + Offset, y, value[y]);
                }
            }
        }

        public Span<T> GetColumn(int column) => this.grid[column].AsSpan();

        public T[][] this[Range column, Range row]
        {
            get => this.grid[column][row];
            //set
            //{
            //    this.SetRange(column, value);
            //}
        }

        public T[][] this[Range columns]
        {
            get
            {
                var (Offset, Length) = columns.GetOffsetAndLength(this.Width);
                var tempColumns = new T[Length][];
                for (var i = 0; i < Length; i++)
                {
                    tempColumns[i] = this.grid[i + Offset];
                }

                return tempColumns;
            }
        }

        public T this[int x, int y]
        {
            get => this.grid[x][y];
            set
            {
                this.grid[x][y] = value;
                this.Changed(x, y, value);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LayerCollection()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
