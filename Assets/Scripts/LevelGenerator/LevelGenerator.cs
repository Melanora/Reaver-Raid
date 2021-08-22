using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{

    public enum EnemyType {Heli = 0, Ship, Balloon, Fuel, Gunner, Jet, Tank}

//    public enum Mode {Empty, Populated};
    [SerializeField] private TileBase _tile;
    [SerializeField] Vector2Int _tileSize;
    [SerializeField] private Vector2Int _LFSR_Seed;
    [SerializeField] private int _easyLevels = 8;
    [SerializeField] private int _chunkLines = 8; // lines
//    [SerializeField] private bool _useSetBlock = false;
//    [SerializeField] private bool _useCoroutine = false;
//    [SerializeField] private bool _manualColliderUpdate = false;
//  	[SerializeField] private bool _inMenu = false;

    [Header("Level")]
    [SerializeField] private int _blocksInLevel = 32; // in blocks;
    [SerializeField] private int _blocksToEndLevel = 3;
    [SerializeField] private int _levelWidth; 
//    [SerializeField] private bool _isTopLevel; 
    
    

    [Header("River")]
    [SerializeField] private int _maxLevelWideWidth = 18;
    [SerializeField] private int _minLevelWideWidth = 2;    
    [SerializeField] private int _levelStraightWidth = 10;
    [SerializeField] private int _easyLevelsWidth = 6;

    [Header("Islands")]
    [SerializeField] private int _minIslandStartBlock = 2;
    [SerializeField] private int _maxIslandStartBlock = 26;
    [SerializeField] private int _minIslandWidth = 2;
    [SerializeField] private int _minIslandGap = 4;
    [SerializeField] private int _minRiverToStartIsland = 18;
    [SerializeField] private int _blocksToEndIsland = 4;
//    [SerializeField] private int _easyIslandWidth = 16;
    
    private Transform _tr;
    private CompositeCollider2D _cc;
    private TilemapCollider2D _tc;
    private int _levelNumber;
    private int _currentLine = 0; 
    private int _currentIslandLine = 0; 
    private int _currentBlock = 0;
    private int _currentLineWidth;
//    private int _riverWidthNew, _riverWidthPrev;
    private int _riverWidth;
    private int _direction; // 1 or -1
    private int _directionPrev;
//    private int _blockOffset; // 0..4
//    private int _offsetTotal, offsetTotalPrev;
    private int _topBridgeWidth;
    private RRlevel _rrLevel;
    private int _linesInLevel;
    private int[] _levelDataRiver;
    private int[] _levelDataIsland;
    private int[] _blocksData;
//    private int[] _enemiesData;
    private Tilemap _tilemap;
    private BoundsInt _bounds;
    private BoundsInt _chunkBounds;
    private int _tilesGeneratedCount;
    private Action GenerateBlock;
    private LFSR _lfsrLevel, _lfsrEnemies;
    private TileBase[] _tileArrayTemp;
    private TileBase[] _tileChunkTemp;

//    private float timeStart, timeEnd, timeElapsed;
    

    void Awake()
    {
        _tr = transform;
//        _cc = GetComponent<CompositeCollider2D>();
//        _tc = GetComponent<TilemapCollider2D>();
        _linesInLevel = _blocksInLevel * RRStaticData.BlockHeight;
        _levelDataRiver = new int[_linesInLevel];
        _levelDataIsland = new int[_linesInLevel];
//        _enemiesData = new int[_blocksInLevel];
        _blocksData = new int[_blocksInLevel];
        _tileArrayTemp = new TileBase[_blocksInLevel * RRStaticData.BlockHeight * _levelWidth];
        _tileChunkTemp = new TileBase[_chunkLines * RRStaticData.BlockHeight * _levelWidth];        
        _rrLevel = GetComponent<RRlevel>();
        _tilemap= GetComponent<Tilemap>();
        _lfsrLevel = new LFSR();
        _lfsrLevel.Seed(_LFSR_Seed.x, _LFSR_Seed.y);
        _lfsrEnemies = new LFSR();
        _lfsrEnemies.Seed(_LFSR_Seed.x, _LFSR_Seed.y);

        _bounds = new BoundsInt(new Vector3Int(-_levelWidth/2, 0, 0), 
                                new Vector3Int(_levelWidth, _blocksInLevel*RRStaticData.BlockHeight, 1));        

    }

    void Start()
    {
//        GenerateLevel(_levelNumber);

    }

    void GetLFSRFromNumber(int seed)
    {
        UnityEngine.Random.InitState(seed);
        int r = UnityEngine.Random.Range(1, 32767);
        _lfsrLevel.Seed((r>>8)&255, r&255);
        _lfsrEnemies.Seed((r>>8)&255, r&255);
    }

    public void GenerateLevel(int level)
    {
//        Debug.Log("Generating level " + level);
//        timeStart = Time.realtimeSinceStartup;

        _levelNumber = level;
        _tilesGeneratedCount = 0;
        GetLFSRFromNumber(level);
        _tile = TileSelector.Selector.GetNewTile();


        if(IsStraightLevel(_levelNumber) || _levelNumber == 0)
            GenerateBlock = GenerateBlockStraight;
        else
            GenerateBlock = GenerateBlockWide;

        ClearIslandData();
        ClearTileArray();
        GenerateRiverData();
        GenerateIslandData();
        StartCoroutine(GenerateTileBlocks());
        GenerateEnemies();
        
//        Debug.Log($"Level: {_levelNumber} Tiles generated: {_tilesGeneratedCount}");
//        timeElapsed = Time.realtimeSinceStartup - timeStart;
//        Debug.Log("Generator time: " + timeElapsed);
    }

    void GenerateRiverData()
    {
        _currentLine = 0;
        _currentBlock = 0;

        ClearRiverData();
        GenerateBlockBridge(false);
        for(int i = 0; i < _blocksInLevel-2; i++) GenerateBlock();
        GenerateBlockBridge(true);
        FixTopRiverGap();
    }

 
    void GenerateBlockBridge(bool inverted)
    {
        int line;
        int lines = RRStaticData.BlockHeight;

        if(!inverted) 
        {
            for(line = 0; line < lines; line++) 
            {
                _riverWidth = RRStaticData.BridgeVariants[0, line];
                _levelDataRiver[_currentLine] = _riverWidth;
                _currentLine++;
            }                
        }
        else
            {
                for(line = lines-1; line >= 0; line--)
                {
                    _riverWidth = RRStaticData.BridgeVariants[0, line];
                    _levelDataRiver[_currentLine] = _riverWidth;
                    _currentLine++;
                }                
                _topBridgeWidth = _riverWidth;
            }

        _currentBlock++;
    }

    void GenerateBlockWide()
    {
        int line;
        int blockVariant;
        int widthNew;

        _lfsrLevel.Next();
        if(CheckRandomize()) _lfsrLevel.Next();
		
        _direction = GetDirection();
        if(Mathf.Abs(_direction - _directionPrev) > 1) _direction = 0;
        blockVariant = GetBlock();
        if(_currentBlock >= _blocksInLevel-_blocksToEndLevel)
            blockVariant = Mathf.Clamp(blockVariant, -1, 0);

        for(line = 0; line < RRStaticData.BlockHeight; line++)
        {
            widthNew = _riverWidth + RRStaticData.Variants[blockVariant, line] * _direction;

            if(widthNew < _maxLevelWideWidth && widthNew > _minLevelWideWidth)
            {
                if(_levelNumber <= _easyLevels && widthNew < _easyLevelsWidth)
                    _riverWidth = _easyLevelsWidth;
                else
                    _riverWidth = widthNew;
            }

            _levelDataRiver[_currentLine] = _riverWidth;
            _currentLine++;
        }

        if(_riverWidth > _levelDataRiver[_currentLine - RRStaticData.BlockHeight])
            _blocksData[_currentBlock] = 1;
        else
            if(_riverWidth < _levelDataRiver[_currentLine - RRStaticData.BlockHeight])
                _blocksData[_currentBlock] = -1;
            else
                _blocksData[_currentBlock] = 0;

        _directionPrev = _direction;
        _currentBlock++;
    }

    void GenerateBlockStraight()
    {
        for(int line = 0; line < RRStaticData.BlockHeight; line++)
        {
            if(_riverWidth < _levelStraightWidth)
                _riverWidth += 2;
            else if(_riverWidth > _levelStraightWidth)
                _riverWidth -= 2;

            _levelDataRiver[_currentLine] = _riverWidth;
            _currentLine++;
        }
        _currentBlock++;
    }

    void GenerateIslandData()
    {
        if(IsStraightLevel(_levelNumber)) return;

        bool islandStarted = false;
        bool islandEnding = false;
        int islandDirection, islandDirectionPrev;
        int islandWidth;
        int blockVariant = 0;
        int islandBlock;
        int line = 0;

        islandWidth = 0;
        islandDirection = 0;
        islandDirectionPrev = 0;

        for(islandBlock = _minIslandStartBlock; islandBlock < _blocksInLevel -1; islandBlock++)
        {
            _lfsrLevel.Next();
            _currentIslandLine = islandBlock * RRStaticData.BlockHeight;

            if(!islandStarted)
            {
                if(IslandCanStart(islandBlock))
                {
                    islandStarted = true;
                    islandWidth = 0; //_minIslandWidth;
                    islandDirection = 1;
                    blockVariant = GetBlock();
//                    Debug.Log($"L: {_levelNumber} Starting new island with block {blockVariant}");
                }
            }
            else // started
            {
                if(!islandEnding)
                {
                    if(_blocksData[islandBlock +2] < 0 || islandBlock >= _blocksInLevel-_blocksToEndIsland)
                    {
                        islandEnding = true;
                        islandDirection = 0; //-1;
                        blockVariant = 4;
//                        Debug.Log($"L: {_levelNumber} Island ending from block {islandBlock}");

                    }
                    else // block and direction selection
                        {
                            islandDirection = GetDirection();
                            if(Mathf.Abs(islandDirection - islandDirectionPrev) > 1) islandDirection = 0;
                            if(islandDirection > 0 && _blocksData[islandBlock +2] < 0) islandDirection = 0;
                            blockVariant = GetBlock();

                            // randomly end island?
                            if((_blocksData[islandBlock-1] < 0) && ((_lfsrLevel.low&128) > 0)) islandEnding = true;
                        }
                }
                else // ending
                    {
                        if(islandWidth > _minIslandWidth)
                        {
                            blockVariant = 4;
                            islandDirection = -1;
                        }
                        else
                            {
                                islandEnding = false;
                                islandStarted = false;
                                blockVariant = 0;
                                islandDirection = 0;
//                                Debug.Log($"L: {_levelNumber} Island ended at block {islandBlock}");
                            }
                    }

                // fill island data

            }

            islandDirectionPrev = islandDirection;

            if(islandStarted)
                for(line = 0; line < RRStaticData.BlockHeight; line++)
                {
                    islandWidth = islandWidth + RRStaticData.Variants[blockVariant, line] *islandDirection;
                    
                    if(islandWidth > (_levelDataRiver[_currentIslandLine] -_minIslandGap))
                        islandWidth = _levelDataRiver[_currentIslandLine] -_minIslandGap;
                    else
                    if(islandWidth < _minIslandWidth) 
                        islandWidth = (islandEnding)? 0 : _minIslandWidth;

                    _levelDataIsland[_currentIslandLine] = islandWidth;
                    _currentIslandLine++;
                }

        }

    }

    void FixTopRiverGap()
    {
        int gap;
        int line = _linesInLevel - RRStaticData.BlockHeight;
        _riverWidth = _topBridgeWidth;

        do {     
            gap = _levelDataRiver[line] - _riverWidth; 

            if(gap < 0) _riverWidth -= 2;
            else 
                if(gap > 0) _riverWidth += 2;

            _levelDataRiver[line] = _riverWidth;
            line--;
        }
        while(gap != 0);
    }

    public IEnumerator GenerateTileBlocks()
    {     
        int line, posX;
        int leftLimit = -_levelWidth/2;
        int rightLimit = _levelWidth/2;
        int riverLeft, riverRight;
        int islandLeft, islandRight;
        int offsetX = _levelWidth/2;
        int chunkOffset;
        int chunk;
        Vector3Int boundsPos, boundsSize;

        _tilemap.ClearAllTiles();
        _tilemap.CompressBounds();

        for(chunk = 0; chunk < (_linesInLevel/_chunkLines); chunk++)
        {
            ClearChunkArray();
            chunkOffset = chunk*_chunkLines;
            for(line = 0; line < _chunkLines; line++)
            {
                riverLeft = -_levelDataRiver[line+chunkOffset]/2;
                riverRight = _levelDataRiver[line+chunkOffset]/2;
                islandLeft = -_levelDataIsland[line+chunkOffset]/2;
                islandRight = _levelDataIsland[line+chunkOffset]/2;

                for(posX = leftLimit; posX < riverLeft; posX++)
                    SetChunkTile(posX +offsetX, line, _tile);

                for(posX = riverRight; posX < rightLimit; posX++)
                    SetChunkTile(posX +offsetX, line, _tile);

                for(posX = islandLeft; posX < islandRight; posX++)
                    SetChunkTile(posX +offsetX, line, _tile);
            }

            boundsPos = new Vector3Int(-_levelWidth/2, chunkOffset, 0);
            boundsSize = new Vector3Int(_levelWidth, _chunkLines*RRStaticData.BlockHeight, 1);
            _chunkBounds = new BoundsInt(boundsPos, boundsSize);        
            _tilemap.SetTilesBlock(_chunkBounds, _tileChunkTemp);
            yield return null;
        }

    }

    void SetChunkTile(int x, int y, TileBase tile)
    {
        _tileChunkTemp[x + y*_levelWidth] = tile;
        _tilesGeneratedCount++;    
    }

    void ClearChunkArray()
    {
        for(int i = 0; i < _tileChunkTemp.Length; i++)
            _tileChunkTemp[i] = null;
    }

    void SetArrayTile(int x, int y, TileBase tile)
    {
        _tileArrayTemp[x + y*_levelWidth] = tile;
        _tilesGeneratedCount++;    
    }

    void ClearTileArray()
    {
        for(int i = 0; i < _tileArrayTemp.Length; i++)
            _tileArrayTemp[i] = null;
    }

    void GenerateEnemies()
    {
        int block;
        GameObject go;
//        RRactor actor;
        PatrolType patrolType;
        Vector3 pos = Vector3.zero;
        int spawnSide;
        int spawnOffset;
        int lineInBLock = 0;
        int riverLine;
        int blockPixelSize = RRStaticData.BlockHeight * _tileSize.y;
        int riverGap;
        int activationDist;
        int enemiesGenerated = 0;

        for(block = 2; block < _blocksInLevel-1; block++)
        {   
            if(_levelNumber <= _easyLevels && block%2 != 0) continue; // general spawn condition

            _lfsrEnemies.Next();
            EnemyType etype = GetEnemy();

            riverLine = block * RRStaticData.BlockHeight +lineInBLock;
            spawnSide = ((_lfsrEnemies.low &1) > 0)? -1: 1;

            if(etype == EnemyType.Jet)
            {
                patrolType = PatrolType.Proximity;
                spawnOffset = _maxLevelWideWidth /2 *_tileSize.x;
                activationDist = 150;
            }
            else
            {
                if(_levelDataIsland[riverLine] == 0)
                {
                    spawnOffset = Mathf.Clamp( _lfsrEnemies.hi &15, 0, _levelDataRiver[riverLine])/2*_tileSize.x - _tileSize.x;

                    if(etype == EnemyType.Balloon || etype == EnemyType.Fuel)
                        spawnOffset -= _tileSize.x;

                    if(_levelDataRiver[riverLine] <= 2)
                        spawnOffset = 1;
                }
                else
                {
                    riverGap = (_levelDataRiver[riverLine] - _levelDataIsland[riverLine]) /2;
                    spawnOffset = _levelDataRiver[riverLine] /2 *_tileSize.x -_tileSize.x;
                    if(etype == EnemyType.Ship) 
                    {
                        if(riverGap <= _minIslandGap && etype == EnemyType.Ship) 
                            etype = EnemyType.Heli;
//                        else spawnOffset -= _tileSize.x;

                    }
                }
                patrolType = GetPatrolType();
                activationDist = _lfsrEnemies.hi&127;
            }

            pos.x = spawnOffset * spawnSide;
            pos.y = block*blockPixelSize -lineInBLock*_tileSize.y +_tr.position.y;// +_tileSize.y/2;
            pos.z = -5;

            go = EnemyPool.Pool.Get( etype.ToString() );
            if(go != null)
            {                    
                go.GetComponent<RRactor>().Init(_tr, patrolType, activationDist, pos);
                go.SetActive(true);
                enemiesGenerated++;
            }
//            else Debug.Log($"Level {_levelNumber} Block {block} Null from pool");

        }
//        Debug.Log($"Level {_levelNumber}. Enemies generated: {enemiesGenerated}");
    }


    bool IsStraightLevel(int level)
    {
        return (level%2) != 0;
    }

    bool CheckRandomize()
    {
        return (_lfsrLevel.low & 48) == 0;
    }

    int GetDirection()
    {
         return Mathf.Clamp((_lfsrLevel.low & 3)-1 , -1, 1);
    }

    int GetRandomRiverState()
    {
        return (int)UnityEngine.Random.Range(-1, 2);
    }

    int GetBlock()
    {
        return Mathf.Clamp((_lfsrLevel.low & 12)>>1, 1, 4);
    }

    void ClearRiverData()
    {
        for(int i = 0; i < _blocksInLevel; i++)
            _blocksData[i] = 0;
    }

    void ClearIslandData()
    {
        for(int i = 0; i < _linesInLevel; i++)
            _levelDataIsland[i] = 0;
    }

    bool IslandCanStart(int blockNum)
    {

        if(blockNum > _maxIslandStartBlock) return false;

        if((_lfsrLevel.low & 128) == 0) return false; 

        if(_levelDataRiver[blockNum*4] < _minRiverToStartIsland) return false;

        if((_blocksData[blockNum] < 0)
            || (_blocksData[blockNum +1] < 0)
            || (_blocksData[blockNum +2] < 0))
            return false;

        return true;
    }

    EnemyType GetEnemy()
    {
        int r = (_levelNumber <= _easyLevels)? 3 : 5;
        return (EnemyType)Mathf.Clamp(_lfsrEnemies.hi&7, 0, r);
    }

    PatrolType GetPatrolType()
    {
        return (PatrolType)Mathf.Clamp((_lfsrEnemies.hi&48)>>4, 0, 2);
    }


}
