using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

public sealed class PathMoveBlock : MonoBehaviourExt
{
    private const float MOVE_SPEED_BLOCKS = 2500f;
    
    private Vector3 _targetPosition;
    private List<Transform> _blocksTransform;

    public void Init(Block[] blocks, Vector2 targetPosition)
    {
        _blocksTransform = new List<Transform>();
        
        foreach (var block in blocks)
        {
            _blocksTransform.Add(block.transform);
        }
        
        _targetPosition = targetPosition;
    }
    
    public void MoveBlocks()
    {
        Path = CPath.Create();
        
        Path
            .EasingLinear(10, 0, 1, f =>
            {
                foreach (var blockTransform in _blocksTransform)
                {
                    if (blockTransform.localPosition.y <= _targetPosition.y)
                    {
                        blockTransform.SetAsFirstSibling();
                    }
                    
                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y - MOVE_SPEED_BLOCKS * Time.deltaTime);
                }
            })
            .Action(MoveBlocks);
    }

    public void StopBlocks()
    {
        Path = null;
    }
}
