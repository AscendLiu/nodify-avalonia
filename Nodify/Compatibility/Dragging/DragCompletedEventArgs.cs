using System;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Nodify.Compatibility;

/// <summary>
/// This DragCompletedEventArgs class contains additional information about the
/// DragCompleted event.
/// </summary>
/// <seealso cref="Thumb.DragCompletedEvent" />
/// <seealso cref="RoutedEventArgs" />
public class DragCompletedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// This is an instance constructor for the DragCompletedEventArgs class.  It
    /// is constructed with a reference to the event being raised.
    /// </summary>
    /// <returns>Nothing.</returns>
    public DragCompletedEventArgs(double horizontalChange, double verticalChange, bool canceled) : base()
    {
        _horizontalChange = horizontalChange;
        _verticalChange = verticalChange;
        _wasCanceled = canceled;
        RoutedEvent=Thumb.DragCompletedEvent;
    }

    /// <value>
    /// Read-only access to the horizontal distance between the point where mouse's left-button
    /// was pressed and the point where mouse's left-button was released
    /// </value>
    public double HorizontalChange
    {
        get { return _horizontalChange; }
    }

    /// <value>
    /// Read-only access to the vertical distance between the point where mouse's left-button
    /// was pressed and the point where mouse's left-button was released
    /// </value>
    public double VerticalChange
    {
        get { return _verticalChange; }
    }

    /// <summary>
    /// Read-only access to boolean states whether the drag operation was canceled or not.
    /// </summary>
    /// <value></value>
    public bool Canceled
    {
        get { return _wasCanceled; }
    }
        
    private double _horizontalChange;
    private double _verticalChange;
    private bool _wasCanceled;
}

/// <summary>
///     This delegate must used by handlers of the DragCompleted event.
/// </summary>
/// <param name="sender">The current element along the event's route.</param>
/// <param name="e">The event arguments containing additional information about the event.</param>
/// <returns>Nothing.</returns>
public delegate void DragCompletedEventHandler(object sender, DragCompletedEventArgs e);