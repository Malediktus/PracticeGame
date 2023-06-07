/// <summary>
/// Empty interface to use as a check when a GameObject is colliding with a PowerupPickup.
/// Every "entity" that can take powerups (mainly the player) should derive from this interface.
/// </summary>
public interface IPowerable
{
    
}

/*
 * Example with interfaces : you want to make objects that explodes, like barrels, bombs or cats (you monster).
 * Instead of doing :
 * 
 * if (gameObject.tag == "Explode") {
 *  gameObject.GetComponent<???>().Explode();
 * }
 * 
 * Because, you need to change the GetComponent type for each object that can explode and you don't know if the Explode method
 * is implemented, you can do :
 * 
 * if (gameObject.TryGetComponent<IExplodable>(out IExplodable explodable) {
 *    explodable.Explode();
 * }
 * 
 * Because when you define your interface, you can't set methods that all classes that will derive from it needs to have the method.
*/