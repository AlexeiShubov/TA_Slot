using System;
using System.Collections.Generic;
using System.Linq;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.UI;

public class PathStoppingBlock : MonoBehaviourExt
{
    private const float MOVE_SPEED_BLOCKS = 2500f;
    private const float MOVE_SPEED_SPRING_OFFSET = 0.2f;
    private const float _ADDITIONAL_TIME_DELAY = 3f;
    private const float _MIDLE_OFFSET = 100f;

    private Block[] _blocks;
    private Vector3 _midlePosition;
    private Vector3 _offsetPosition;
    private Vector3 _targetPosition;
    private Vector3 _topPosition;
    private List<Transform> _blocksTransform;
    private GridLayoutGroup _gridLayoutGroup;

    public void Init(Block[] blocks, Vector2 targetPosition, Vector2 topPosition, GridLayoutGroup gridLayoutGroup)
    {
        _blocks = blocks;
        _blocksTransform = new List<Transform>();
        _gridLayoutGroup = gridLayoutGroup;
        
        foreach (var block in blocks)
        {
            _blocksTransform.Add(block.transform);
        }

        _midlePosition = _blocksTransform[^2].localPosition;
        _offsetPosition = _midlePosition - new Vector3(0f, _MIDLE_OFFSET);

        _targetPosition = targetPosition;
        _topPosition = topPosition;
    }
    
    public void StoppingBlocks(int winningID, Action callback)
    {
        CreatePath();
        var winningBlock = _blocks.First(t => t.ID == winningID);
        var winningTransform = winningBlock.transform;

        Path
            .EasingLinear(_ADDITIONAL_TIME_DELAY, 0, 1, f =>
            {
                foreach (var blockTransform in _blocksTransform)
                {
                    if (blockTransform.localPosition.y <= _targetPosition.y)
                    {
                        blockTransform.SetAsFirstSibling();
                    }

                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y - MOVE_SPEED_BLOCKS * Time.deltaTime);
                }
                
                if (winningTransform.localPosition.y <= _offsetPosition.y)
                {
                    Spring(winningBlock, callback);
                }
            });
    }

    private void Spring(Block winningBlock, Action callback)
    {
        CreatePath();

        Path
            .Wait(0.05f)
            .EasingLinear(_ADDITIONAL_TIME_DELAY, 0, 1, f =>
            {
                foreach (var blockTransform in _blocksTransform)
                {
                    if (blockTransform.localPosition.y >= _topPosition.y)
                    {
                        blockTransform.SetAsLastSibling();
                    }
                    
                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y + MOVE_SPEED_BLOCKS * MOVE_SPEED_SPRING_OFFSET * Time.deltaTime);
                }
                
                if (winningBlock.transform.localPosition.y >= _midlePosition.y)
                {
                    _gridLayoutGroup.enabled = false;
                    _gridLayoutGroup.enabled = true;
                    winningBlock.ActiveFrameEffect(true);
                    callback?.Invoke();
                    Path = null;
                }
            });
    }

    private void CreatePath()
    {
        Path = null;
        Path = CPath.Create();
    }
}
