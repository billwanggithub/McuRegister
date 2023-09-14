
# Programming Note

## DataTemplate

- DataType
   <br>specify the .NET type of the data object that the template should be applied to
    ```
    <DataTemplate DataType="{x:Type local:MyDataObject}">
    <!-- Define your UI elements here -->
    </DataTemplate>
    ```
- 常用於ListView/ItemsControl
    ```
    <Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:YourNamespace"
        Title="DataTemplate Example" Height="350" Width="525">
    <Grid>
        <ListView ItemsSource="{Binding YourCollection}">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Name" Width="120">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate DataType="{x:Type local:Person}">
                                <TextBlock Text="{Binding Name}"/>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                    <!-- Add more columns and templates as needed -->
                </GridView>
            </ListView.View>
        </ListView>
    </Grid>
</Window>
    ```

## Composition Collection

```
CompositeCollection compositeCollection = new CompositeCollection();
ObservableCollection<InboundMessage> inboundMessages = new ObservableCollection<InboundMessage>();
ObservableCollection<OutboundMessage> outboundMessages = new ObservableCollection<OutboundMessage>();
compositeCollection.Add(new CollectionContainer() { Collection = inboundMessages });
compositeCollection.Add(new CollectionContainer() { Collection = outboundMessages });
conversationList.ItemsSource = compositeCollection;
```

## References:
- [Overview Of Composite Collection In WPF](https://www.c-sharpcorner.com/article/overview-of-composite-collection-in-wpf/)
- [How to: Implement a CompositeCollection](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-a-compositecollection?view=netframeworkdesktop-4.8)
- [Binding時，依照DataType選擇DataTemplate](https://dotblogs.com.tw/clark/2014/02/19/144072)
- [將樹狀檢視繫結至未知深度的資料](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-bind-a-treeview-to-data-that-has-an-indeterminable-depth?view=netframeworkdesktop-4.8)