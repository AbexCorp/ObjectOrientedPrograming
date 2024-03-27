using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Node<T>
{    
    public T Data {get; set;}
    public Node<T> Next {get; set;}
    public Node(T data, Node<T> next = null) {
        Data = data; Next = next;
    }
    public override string ToString() => (this==null)? "null" : $"{Data} -> ";
}