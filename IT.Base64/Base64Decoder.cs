﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Base64;

public readonly struct Base64Decoder
{
    public static readonly Base64Decoder Default = new([
    -1, //0
    -1, //1
    -1, //2
    -1, //3
    -1, //4
    -1, //5
    -1, //6
    -1, //7
    -1, //8
    -1, //9
    -1, //10
    -1, //11
    -1, //12
    -1, //13
    -1, //14
    -1, //15
    -1, //16
    -1, //17
    -1, //18
    -1, //19
    -1, //20
    -1, //21
    -1, //22
    -1, //23
    -1, //24
    -1, //25
    -1, //26
    -1, //27
    -1, //28
    -1, //29
    -1, //30
    -1, //31
    -1, //32
    -1, //33
    -1, //34
    -1, //35
    -1, //36
    -1, //37
    -1, //38
    -1, //39
    -1, //40
    -1, //41
    -1, //42
    62, //43 -> +
    -1, //44
    -1, //45 -> -
    -1, //46
    63, //47 -> /
    52, //48 -> 0
    53, //49 -> 1
    54, //50 -> 2
    55, //51 -> 3
    56, //52 -> 4
    57, //53 -> 5
    58, //54 -> 6
    59, //55 -> 7
    60, //56 -> 8
    61, //57 -> 9
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
     0, //65 -> A
     1, //66 -> B
     2, //67 -> C
     3, //68 -> D
     4, //69 -> E
     5, //70 -> F
     6, //71 -> G
     7, //72 -> H
     8, //73 -> I
     9, //74 -> J
    10, //75 -> K
    11, //76 -> L
    12, //77 -> M
    13, //78 -> N
    14, //79 -> O
    15, //80 -> P
    16, //81 -> Q
    17, //82 -> R
    18, //83 -> S
    19, //84 -> T
    20, //85 -> U
    21, //86 -> V
    22, //87 -> W
    23, //88 -> X
    24, //89 -> Y
    25, //90 -> Z
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    -1, //95 -> _
    -1, //96
    26, //97 -> a
    27, //98 -> b
    28, //99 -> c
    29, //100 -> d
    30, //101 -> e
    31, //102 -> f
    32, //103 -> g
    33, //104 -> h
    34, //105 -> i
    35, //106 -> j
    36, //107 -> k
    37, //108 -> l
    38, //109 -> m
    39, //110 -> n
    40, //111 -> o
    41, //112 -> p
    42, //113 -> q
    43, //114 -> r
    44, //115 -> s
    45, //116 -> t
    46, //117 -> u
    47, //118 -> v
    48, //119 -> w
    49, //120 -> x
    50, //121 -> y
    51, //122 -> z
    -1, //123
    -1, //124
    -1, //125
    -1, //126
    -1, //127
    -1, //128
    -1, //129
    -1, //130
    -1, //131
    -1, //132
    -1, //133
    -1, //134
    -1, //135
    -1, //136
    -1, //137
    -1, //138
    -1, //139
    -1, //140
    -1, //141
    -1, //142
    -1, //143
    -1, //144
    -1, //145
    -1, //146
    -1, //147
    -1, //148
    -1, //149
    -1, //150
    -1, //151
    -1, //152
    -1, //153
    -1, //154
    -1, //155
    -1, //156
    -1, //157
    -1, //158
    -1, //159
    -1, //160
    -1, //161
    -1, //162
    -1, //163
    -1, //164
    -1, //165
    -1, //166
    -1, //167
    -1, //168
    -1, //169
    -1, //170
    -1, //171
    -1, //172
    -1, //173
    -1, //174
    -1, //175
    -1, //176
    -1, //177
    -1, //178
    -1, //179
    -1, //180
    -1, //181
    -1, //182
    -1, //183
    -1, //184
    -1, //185
    -1, //186
    -1, //187
    -1, //188
    -1, //189
    -1, //190
    -1, //191
    -1, //192
    -1, //193
    -1, //194
    -1, //195
    -1, //196
    -1, //197
    -1, //198
    -1, //199
    -1, //200
    -1, //201
    -1, //202
    -1, //203
    -1, //204
    -1, //205
    -1, //206
    -1, //207
    -1, //208
    -1, //209
    -1, //210
    -1, //211
    -1, //212
    -1, //213
    -1, //214
    -1, //215
    -1, //216
    -1, //217
    -1, //218
    -1, //219
    -1, //220
    -1, //221
    -1, //222
    -1, //223
    -1, //224
    -1, //225
    -1, //226
    -1, //227
    -1, //228
    -1, //229
    -1, //230
    -1, //231
    -1, //232
    -1, //233
    -1, //234
    -1, //235
    -1, //236
    -1, //237
    -1, //238
    -1, //239
    -1, //240
    -1, //241
    -1, //242
    -1, //243
    -1, //244
    -1, //245
    -1, //246
    -1, //247
    -1, //248
    -1, //249
    -1, //250
    -1, //251
    -1, //252
    -1, //253
    -1, //254
    -1, //255
    ]);
    public static readonly Base64Decoder Url = new([
    -1, //0
    -1, //1
    -1, //2
    -1, //3
    -1, //4
    -1, //5
    -1, //6
    -1, //7
    -1, //8
    -1, //9
    -1, //10
    -1, //11
    -1, //12
    -1, //13
    -1, //14
    -1, //15
    -1, //16
    -1, //17
    -1, //18
    -1, //19
    -1, //20
    -1, //21
    -1, //22
    -1, //23
    -1, //24
    -1, //25
    -1, //26
    -1, //27
    -1, //28
    -1, //29
    -1, //30
    -1, //31
    -1, //32
    -1, //33
    -1, //34
    -1, //35
    -1, //36
    -1, //37
    -1, //38
    -1, //39
    -1, //40
    -1, //41
    -1, //42
    -1, //43 -> +
    -1, //44
    62, //45 -> -
    -1, //46
    -1, //47 -> /
    52, //48 -> 0
    53, //49 -> 1
    54, //50 -> 2
    55, //51 -> 3
    56, //52 -> 4
    57, //53 -> 5
    58, //54 -> 6
    59, //55 -> 7
    60, //56 -> 8
    61, //57 -> 9
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
     0, //65 -> A
     1, //66 -> B
     2, //67 -> C
     3, //68 -> D
     4, //69 -> E
     5, //70 -> F
     6, //71 -> G
     7, //72 -> H
     8, //73 -> I
     9, //74 -> J
    10, //75 -> K
    11, //76 -> L
    12, //77 -> M
    13, //78 -> N
    14, //79 -> O
    15, //80 -> P
    16, //81 -> Q
    17, //82 -> R
    18, //83 -> S
    19, //84 -> T
    20, //85 -> U
    21, //86 -> V
    22, //87 -> W
    23, //88 -> X
    24, //89 -> Y
    25, //90 -> Z
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    63, //95 -> _
    -1, //96
    26, //97 -> a
    27, //98 -> b
    28, //99 -> c
    29, //100 -> d
    30, //101 -> e
    31, //102 -> f
    32, //103 -> g
    33, //104 -> h
    34, //105 -> i
    35, //106 -> j
    36, //107 -> k
    37, //108 -> l
    38, //109 -> m
    39, //110 -> n
    40, //111 -> o
    41, //112 -> p
    42, //113 -> q
    43, //114 -> r
    44, //115 -> s
    45, //116 -> t
    46, //117 -> u
    47, //118 -> v
    48, //119 -> w
    49, //120 -> x
    50, //121 -> y
    51, //122 -> z
    -1, //123
    -1, //124
    -1, //125
    -1, //126
    -1, //127
    -1, //128
    -1, //129
    -1, //130
    -1, //131
    -1, //132
    -1, //133
    -1, //134
    -1, //135
    -1, //136
    -1, //137
    -1, //138
    -1, //139
    -1, //140
    -1, //141
    -1, //142
    -1, //143
    -1, //144
    -1, //145
    -1, //146
    -1, //147
    -1, //148
    -1, //149
    -1, //150
    -1, //151
    -1, //152
    -1, //153
    -1, //154
    -1, //155
    -1, //156
    -1, //157
    -1, //158
    -1, //159
    -1, //160
    -1, //161
    -1, //162
    -1, //163
    -1, //164
    -1, //165
    -1, //166
    -1, //167
    -1, //168
    -1, //169
    -1, //170
    -1, //171
    -1, //172
    -1, //173
    -1, //174
    -1, //175
    -1, //176
    -1, //177
    -1, //178
    -1, //179
    -1, //180
    -1, //181
    -1, //182
    -1, //183
    -1, //184
    -1, //185
    -1, //186
    -1, //187
    -1, //188
    -1, //189
    -1, //190
    -1, //191
    -1, //192
    -1, //193
    -1, //194
    -1, //195
    -1, //196
    -1, //197
    -1, //198
    -1, //199
    -1, //200
    -1, //201
    -1, //202
    -1, //203
    -1, //204
    -1, //205
    -1, //206
    -1, //207
    -1, //208
    -1, //209
    -1, //210
    -1, //211
    -1, //212
    -1, //213
    -1, //214
    -1, //215
    -1, //216
    -1, //217
    -1, //218
    -1, //219
    -1, //220
    -1, //221
    -1, //222
    -1, //223
    -1, //224
    -1, //225
    -1, //226
    -1, //227
    -1, //228
    -1, //229
    -1, //230
    -1, //231
    -1, //232
    -1, //233
    -1, //234
    -1, //235
    -1, //236
    -1, //237
    -1, //238
    -1, //239
    -1, //240
    -1, //241
    -1, //242
    -1, //243
    -1, //244
    -1, //245
    -1, //246
    -1, //247
    -1, //248
    -1, //249
    -1, //250
    -1, //251
    -1, //252
    -1, //253
    -1, //254
    -1, //255
    ]);
    public static readonly Base64Decoder Any = new([
    -1, //0
    -1, //1
    -1, //2
    -1, //3
    -1, //4
    -1, //5
    -1, //6
    -1, //7
    -1, //8
    -1, //9
    -1, //10
    -1, //11
    -1, //12
    -1, //13
    -1, //14
    -1, //15
    -1, //16
    -1, //17
    -1, //18
    -1, //19
    -1, //20
    -1, //21
    -1, //22
    -1, //23
    -1, //24
    -1, //25
    -1, //26
    -1, //27
    -1, //28
    -1, //29
    -1, //30
    -1, //31
    -1, //32
    -1, //33
    -1, //34
    -1, //35
    -1, //36
    -1, //37
    -1, //38
    -1, //39
    -1, //40
    -1, //41
    -1, //42
    62, //43 -> +
    -1, //44
    62, //45 -> -
    -1, //46
    63, //47 -> /
    52, //48 -> 0
    53, //49 -> 1
    54, //50 -> 2
    55, //51 -> 3
    56, //52 -> 4
    57, //53 -> 5
    58, //54 -> 6
    59, //55 -> 7
    60, //56 -> 8
    61, //57 -> 9
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
     0, //65 -> A
     1, //66 -> B
     2, //67 -> C
     3, //68 -> D
     4, //69 -> E
     5, //70 -> F
     6, //71 -> G
     7, //72 -> H
     8, //73 -> I
     9, //74 -> J
    10, //75 -> K
    11, //76 -> L
    12, //77 -> M
    13, //78 -> N
    14, //79 -> O
    15, //80 -> P
    16, //81 -> Q
    17, //82 -> R
    18, //83 -> S
    19, //84 -> T
    20, //85 -> U
    21, //86 -> V
    22, //87 -> W
    23, //88 -> X
    24, //89 -> Y
    25, //90 -> Z
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    63, //95 -> _
    -1, //96
    26, //97 -> a
    27, //98 -> b
    28, //99 -> c
    29, //100 -> d
    30, //101 -> e
    31, //102 -> f
    32, //103 -> g
    33, //104 -> h
    34, //105 -> i
    35, //106 -> j
    36, //107 -> k
    37, //108 -> l
    38, //109 -> m
    39, //110 -> n
    40, //111 -> o
    41, //112 -> p
    42, //113 -> q
    43, //114 -> r
    44, //115 -> s
    45, //116 -> t
    46, //117 -> u
    47, //118 -> v
    48, //119 -> w
    49, //120 -> x
    50, //121 -> y
    51, //122 -> z
    -1, //123
    -1, //124
    -1, //125
    -1, //126
    -1, //127
    -1, //128
    -1, //129
    -1, //130
    -1, //131
    -1, //132
    -1, //133
    -1, //134
    -1, //135
    -1, //136
    -1, //137
    -1, //138
    -1, //139
    -1, //140
    -1, //141
    -1, //142
    -1, //143
    -1, //144
    -1, //145
    -1, //146
    -1, //147
    -1, //148
    -1, //149
    -1, //150
    -1, //151
    -1, //152
    -1, //153
    -1, //154
    -1, //155
    -1, //156
    -1, //157
    -1, //158
    -1, //159
    -1, //160
    -1, //161
    -1, //162
    -1, //163
    -1, //164
    -1, //165
    -1, //166
    -1, //167
    -1, //168
    -1, //169
    -1, //170
    -1, //171
    -1, //172
    -1, //173
    -1, //174
    -1, //175
    -1, //176
    -1, //177
    -1, //178
    -1, //179
    -1, //180
    -1, //181
    -1, //182
    -1, //183
    -1, //184
    -1, //185
    -1, //186
    -1, //187
    -1, //188
    -1, //189
    -1, //190
    -1, //191
    -1, //192
    -1, //193
    -1, //194
    -1, //195
    -1, //196
    -1, //197
    -1, //198
    -1, //199
    -1, //200
    -1, //201
    -1, //202
    -1, //203
    -1, //204
    -1, //205
    -1, //206
    -1, //207
    -1, //208
    -1, //209
    -1, //210
    -1, //211
    -1, //212
    -1, //213
    -1, //214
    -1, //215
    -1, //216
    -1, //217
    -1, //218
    -1, //219
    -1, //220
    -1, //221
    -1, //222
    -1, //223
    -1, //224
    -1, //225
    -1, //226
    -1, //227
    -1, //228
    -1, //229
    -1, //230
    -1, //231
    -1, //232
    -1, //233
    -1, //234
    -1, //235
    -1, //236
    -1, //237
    -1, //238
    -1, //239
    -1, //240
    -1, //241
    -1, //242
    -1, //243
    -1, //244
    -1, //245
    -1, //246
    -1, //247
    -1, //248
    -1, //249
    -1, //250
    -1, //251
    -1, //252
    -1, //253
    -1, //254
    -1, //255
    ]);
    public static readonly Base64Decoder IT = new([
    -1, //0
    -1, //1
    -1, //2
    -1, //3
    -1, //4
    -1, //5
    -1, //6
    -1, //7
    -1, //8
    -1, //9
    -1, //10
    -1, //11
    -1, //12
    -1, //13
    -1, //14
    -1, //15
    -1, //16
    -1, //17
    -1, //18
    -1, //19
    -1, //20
    -1, //21
    -1, //22
    -1, //23
    -1, //24
    -1, //25
    -1, //26
    -1, //27
    -1, //28
    -1, //29
    -1, //30
    -1, //31
    -1, //32
    -1, //33
    -1, //34
    -1, //35
    -1, //36
    -1, //37
    -1, //38
    -1, //39
    -1, //40
    -1, //41
    -1, //42
    -1, //43
    -1, //44
    63, //45 -> -
    -1, //46
    -1, //47
     0, //48 -> 0
     1, //49 -> 1
     2, //50 -> 2
     3, //51 -> 3
     4, //52 -> 4
     5, //53 -> 5
     6, //54 -> 6
     7, //55 -> 7
     8, //56 -> 8
     9, //57 -> 9
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
    36, //65 -> A
    37, //66 -> B
    38, //67 -> C
    39, //68 -> D
    40, //69 -> E
    41, //70 -> F
    42, //71 -> G
    43, //72 -> H
    44, //73 -> I
    45, //74 -> J
    46, //75 -> K
    47, //76 -> L
    48, //77 -> M
    49, //78 -> N
    50, //79 -> O
    51, //80 -> P
    52, //81 -> Q
    53, //82 -> R
    54, //83 -> S
    55, //84 -> T
    56, //85 -> U
    57, //86 -> V
    58, //87 -> W
    59, //88 -> X
    60, //89 -> Y
    61, //90 -> Z
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    62, //95 -> _
    -1, //96
    10, //97 -> a
    11, //98 -> b
    12, //99 -> c
    13, //100 -> d
    14, //101 -> e
    15, //102 -> f
    16, //103 -> g
    17, //104 -> h
    18, //105 -> i
    19, //106 -> j
    20, //107 -> k
    21, //108 -> l
    22, //109 -> m
    23, //110 -> n
    24, //111 -> o
    25, //112 -> p
    26, //113 -> q
    27, //114 -> r
    28, //115 -> s
    29, //116 -> t
    30, //117 -> u
    31, //118 -> v
    32, //119 -> w
    33, //120 -> x
    34, //121 -> y
    35, //122 -> z
    -1, //123
    -1, //124
    -1, //125
    -1, //126
    -1, //127
    -1, //128
    -1, //129
    -1, //130
    -1, //131
    -1, //132
    -1, //133
    -1, //134
    -1, //135
    -1, //136
    -1, //137
    -1, //138
    -1, //139
    -1, //140
    -1, //141
    -1, //142
    -1, //143
    -1, //144
    -1, //145
    -1, //146
    -1, //147
    -1, //148
    -1, //149
    -1, //150
    -1, //151
    -1, //152
    -1, //153
    -1, //154
    -1, //155
    -1, //156
    -1, //157
    -1, //158
    -1, //159
    -1, //160
    -1, //161
    -1, //162
    -1, //163
    -1, //164
    -1, //165
    -1, //166
    -1, //167
    -1, //168
    -1, //169
    -1, //170
    -1, //171
    -1, //172
    -1, //173
    -1, //174
    -1, //175
    -1, //176
    -1, //177
    -1, //178
    -1, //179
    -1, //180
    -1, //181
    -1, //182
    -1, //183
    -1, //184
    -1, //185
    -1, //186
    -1, //187
    -1, //188
    -1, //189
    -1, //190
    -1, //191
    -1, //192
    -1, //193
    -1, //194
    -1, //195
    -1, //196
    -1, //197
    -1, //198
    -1, //199
    -1, //200
    -1, //201
    -1, //202
    -1, //203
    -1, //204
    -1, //205
    -1, //206
    -1, //207
    -1, //208
    -1, //209
    -1, //210
    -1, //211
    -1, //212
    -1, //213
    -1, //214
    -1, //215
    -1, //216
    -1, //217
    -1, //218
    -1, //219
    -1, //220
    -1, //221
    -1, //222
    -1, //223
    -1, //224
    -1, //225
    -1, //226
    -1, //227
    -1, //228
    -1, //229
    -1, //230
    -1, //231
    -1, //232
    -1, //233
    -1, //234
    -1, //235
    -1, //236
    -1, //237
    -1, //238
    -1, //239
    -1, //240
    -1, //241
    -1, //242
    -1, //243
    -1, //244
    -1, //245
    -1, //246
    -1, //247
    -1, //248
    -1, //249
    -1, //250
    -1, //251
    -1, //252
    -1, //253
    -1, //254
    -1, //255
    ]);

    internal readonly sbyte[] _map;

    public ReadOnlyMemory<sbyte> Map => _map;

    public Base64Decoder(ReadOnlySpan<sbyte> map)
    {
        if (map.Length != 256) throw new ArgumentOutOfRangeException(nameof(map), map.Length, "length != 256");

        _map = map.ToArray();
    }

    #region Decode128

    public DecodingStatus TryDecode128(ReadOnlySpan<byte> encoded, out Int128 value)
    {
        value = default;
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode128(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode128(ReadOnlySpan<char> encoded, out Int128 value)
    {
        value = default;
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode128(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode128(ReadOnlySpan<byte> encoded, out Int128 value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode128(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode128(ReadOnlySpan<char> encoded, out Int128 value, out char invalid)
    {
        value = default;
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode128(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public Int128 Decode128(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");

        Int128 value = 0;
        if (!UnsafeBase64.TryDecode128(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public Int128 Decode128(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");

        Int128 value = 0;
        if (!UnsafeBase64.TryDecode128(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode128

    #region Valid128

    public DecodingStatus TryValid128(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid128(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid128(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid128(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid128(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid128(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid128(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid128(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid128(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");
        if (!UnsafeBase64.IsValid128(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid128(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");
        if (!UnsafeBase64.IsValid128(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid128

    #region Decode72

    public DecodingStatus TryDecode72<T>(ReadOnlySpan<byte> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12) return DecodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 9) return DecodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode72(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode72<T>(ReadOnlySpan<char> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12) return DecodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 9) return DecodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode72(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode72<T>(ReadOnlySpan<byte> encoded, out T value, out byte invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 9)
        {
            invalid = default;
            return DecodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode72(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode72<T>(ReadOnlySpan<char> encoded, out T value, out char invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 9)
        {
            invalid = default;
            return DecodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode72(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode72<T>(ReadOnlySpan<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");

        T value = default;
        if (!UnsafeBase64.TryDecode72(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode72<T>(ReadOnlySpan<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");

        T value = default;
        if (!UnsafeBase64.TryDecode72(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");

        return value;
    }

    #endregion Decode72

    #region Valid72

    public DecodingStatus TryValid72(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 12) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid72(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid72(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 12) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid72(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid72(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 12)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid72(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid72(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 12)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid72(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid72(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");
        if (!UnsafeBase64.IsValid72(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid72(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");
        if (!UnsafeBase64.IsValid72(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");
    }

    #endregion Valid72

    #region Decode64

    #region unmanaged

    public DecodingStatus TryDecode64<T>(ReadOnlySpan<byte> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (Unsafe.SizeOf<T>() < 8) return DecodingStatus.InvalidDestinationLength;
        if (encoded.Length < 11) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode64<T>(ReadOnlySpan<char> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (Unsafe.SizeOf<T>() < 8) return DecodingStatus.InvalidDestinationLength;
        if (encoded.Length < 11) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode64<T>(ReadOnlySpan<byte> encoded, out T value, out byte invalid) where T : unmanaged
    {
        value = default;
        if (Unsafe.SizeOf<T>() < 8)
        {
            invalid = default;
            return DecodingStatus.InvalidDestinationLength;
        }
        if (encoded.Length < 11)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode64<T>(ReadOnlySpan<char> encoded, out T value, out char invalid) where T : unmanaged
    {
        value = default;
        if (Unsafe.SizeOf<T>() < 8)
        {
            invalid = default;
            return DecodingStatus.InvalidDestinationLength;
        }
        if (encoded.Length < 11)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode64<T>(ReadOnlySpan<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() < 8) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> < 8");
        if (encoded.Length < 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 11");

        T value = default;
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode64<T>(ReadOnlySpan<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() < 8) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> < 8");
        if (encoded.Length < 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 11");

        T value = default;
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion unmanaged

    #region ulong

    public DecodingStatus TryDecode64(ReadOnlySpan<byte> encoded, out ulong value)
    {
        value = default;
        if (encoded.Length != 11) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode64(ReadOnlySpan<char> encoded, out ulong value)
    {
        value = default;
        if (encoded.Length != 11) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode64(ReadOnlySpan<byte> encoded, out ulong value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 11)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode64(ReadOnlySpan<char> encoded, out ulong value, out char invalid)
    {
        value = default;
        if (encoded.Length != 11)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ulong Decode64(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");

        ulong value = 0;
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ulong Decode64(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");

        ulong value = 0;
        if (!UnsafeBase64.TryDecode64(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion ulong

    #endregion Decode64

    #region Valid64

    public DecodingStatus TryValid64(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 11) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid64(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid64(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 11) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid64(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid64(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 11)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid64(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid64(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 11)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid64(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid64(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");
        if (!UnsafeBase64.IsValid64(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid64(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");
        if (!UnsafeBase64.IsValid64(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid64

    #region Decode32

    public DecodingStatus TryDecode32(ReadOnlySpan<byte> encoded, out uint value)
    {
        value = default;
        if (encoded.Length != 6) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode32(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode32(ReadOnlySpan<char> encoded, out uint value)
    {
        value = default;
        if (encoded.Length != 6) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode32(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode32(ReadOnlySpan<byte> encoded, out uint value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 6)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode32(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode32(ReadOnlySpan<char> encoded, out uint value, out char invalid)
    {
        value = default;
        if (encoded.Length != 6)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode32(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public uint Decode32(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");

        uint value = 0;
        if (!UnsafeBase64.TryDecode32(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public uint Decode32(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");

        uint value = 0;
        if (!UnsafeBase64.TryDecode32(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode32

    #region Valid32

    public DecodingStatus TryValid32(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 6) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid32(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid32(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 6) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid32(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid32(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 6)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid32(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid32(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 6)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid32(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid32(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");
        if (!UnsafeBase64.IsValid32(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid32(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");
        if (!UnsafeBase64.IsValid32(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid32

    #region Decode24

    public DecodingStatus TryDecode24<T>(ReadOnlySpan<byte> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4) return DecodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 3) return DecodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode24(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode24<T>(ReadOnlySpan<char> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4) return DecodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 3) return DecodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode24(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode24<T>(ReadOnlySpan<byte> encoded, out T value, out byte invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 3)
        {
            invalid = default;
            return DecodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode24(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode24<T>(ReadOnlySpan<char> encoded, out T value, out char invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 3)
        {
            invalid = default;
            return DecodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode24(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode24<T>(ReadOnlySpan<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");

        T value = default;
        if (!UnsafeBase64.TryDecode24(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode24<T>(ReadOnlySpan<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");

        T value = default;
        if (!UnsafeBase64.TryDecode24(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");

        return value;
    }

    #endregion Decode24

    #region Valid24

    public DecodingStatus TryValid24(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 4) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid24(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid24(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 4) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid24(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid24(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 4)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid24(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid24(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 4)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid24(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid24(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");
        if (!UnsafeBase64.IsValid24(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid24(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");
        if (!UnsafeBase64.IsValid24(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");
    }

    #endregion Valid24

    #region Decode16

    public DecodingStatus TryDecode16(ReadOnlySpan<byte> encoded, out ushort value)
    {
        value = default;
        if (encoded.Length != 3) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode16(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode16(ReadOnlySpan<char> encoded, out ushort value)
    {
        value = default;
        if (encoded.Length != 3) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode16(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode16(ReadOnlySpan<byte> encoded, out ushort value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 3)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode16(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode16(ReadOnlySpan<char> encoded, out ushort value, out char invalid)
    {
        value = default;
        if (encoded.Length != 3)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode16(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ushort Decode16(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");

        ushort value = 0;
        if (!UnsafeBase64.TryDecode16(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ushort Decode16(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");

        ushort value = 0;
        if (!UnsafeBase64.TryDecode16(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode16

    #region Valid16

    public DecodingStatus TryValid16(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 3) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid16(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid16(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 3) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid16(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid16(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 3)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid16(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid16(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 3)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid16(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid16(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");
        if (!UnsafeBase64.IsValid16(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid16(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");
        if (!UnsafeBase64.IsValid16(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid16

    #region Decode8

    public DecodingStatus TryDecode8(ReadOnlySpan<byte> encoded, out byte value)
    {
        value = default;
        if (encoded.Length != 2) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode8(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref value))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode8(ReadOnlySpan<char> encoded, out byte value)
    {
        value = default;
        if (encoded.Length != 2) return DecodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode8(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref value))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode8(ReadOnlySpan<byte> encoded, out byte value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 2)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode8(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref value, out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public DecodingStatus TryDecode8(ReadOnlySpan<char> encoded, out byte value, out char invalid)
    {
        value = default;
        if (encoded.Length != 2)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode8(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref value, out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public byte Decode8(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");

        byte value = 0;
        if (!UnsafeBase64.TryDecode8(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref value, out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public byte Decode8(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");

        byte value = 0;
        if (!UnsafeBase64.TryDecode8(ref _map[0], ref MemoryMarshal.GetReference(encoded), ref value, out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode8

    #region Valid8

    public DecodingStatus TryValid8(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 2) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid8(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid8(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 2) return DecodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid8(ref _map[0], ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid8(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 2)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid8(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public DecodingStatus TryValid8(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 2)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid8(ref _map[0], ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid8(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");
        if (!UnsafeBase64.IsValid8(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid8(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");
        if (!UnsafeBase64.IsValid8(ref _map[0], ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid8
}