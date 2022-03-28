using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    //ref: https://www.geeksforgeeks.org/c-sharp-abstract-classes/

    /*This is the way to achieve the abstraction in C#. 
     * An Abstract class is never intended to be instantiated directly. 
     * Abstract class can also be created without any abstract methods, 
     * We can mark a class abstract even if doesn’t have any abstract method. 
     * The Abstract classes are typically used to define a base class in the class hierarchy. 
     * Or in other words, an abstract class is an incomplete class or special class we 
     * can’t be instantiated. 
     * The purpose of an abstract class is to provide a blueprint for derived classes 
     * and set some rules what the derived classes must implement when they inherit an 
     * abstract class. We can use an abstract class as a base class and all derived 
     * classes must implement abstract definitions. */

    // EntityBase class properties are common to every database table operations.
    // so we declared this class as abstract and it will be inherited by corresponding
    // derived class.
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
