using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletC
{
    protected override void Move()
    {
        transform.Translate(0, speed * Time.deltaTime , 0);
    }
}
